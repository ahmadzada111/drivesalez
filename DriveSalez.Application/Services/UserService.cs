using System.Security.Claims;
using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Application.Services;

internal sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtService _jwtService;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        IJwtService jwtService, IHttpContextAccessor contextAccessor, 
        IFileService fileService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
        _jwtService = jwtService;
        _fileService = fileService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApplicationUser> FindByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new UserNotFoundException("User not found");
        
        return user;
    }

    public async Task<IdentityResult> ChangeEmail(Guid userId, string newEmail, string token)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new UserNotFoundException("User not found.");
        }

        var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

        if (result.Succeeded)
        {
            user.UserName = newEmail;
            await _userManager.UpdateAsync(user);
        }

        return result;
    }
    
    public Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
    {
        return _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public Task<string> GeneratePasswordResetToken(ApplicationUser user)
    {
        return _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public Task<string> GenerateChangeEmailToken(ApplicationUser user, string newEmail)
    {
        return _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
    }

    public async Task<IdentityResult> ConfirmEmail(Guid userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
                   ?? throw new UserNotFoundException("User not found.");

        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    private async Task<IdentityResult> RegisterUser(ApplicationUser identityUser, string password, UserType userType)
    {
        IdentityResult result = await _userManager.CreateAsync(identityUser, password);
        
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(identityUser, userType.ToString());
            await _userManager.UpdateAsync(identityUser);
            // await _userRepository.AddLimitsToAccountInDbAsync(identityUser, userType);
        }

        return result;
    }

    public async Task<IdentityResult> RegisterAccount(RegisterAccountDto request, UserType userType)
    {
        var userProfile = new UserProfile()
        {
            AccountBalance = 0,
            CreationDate = DateTimeOffset.UtcNow,
            PhoneNumbers = _mapper.Map<List<PhoneNumber>>(request.PhoneNumbers)
            
        };
        
        var identityUser = new ApplicationUser()
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName ?? request.Email,
            EmailConfirmed = false,
            BaseUser = userProfile
        };

        return await RegisterUser(identityUser, request.Password, userType);
    }

    private async Task<ApplicationUser> SignInAndValidateUser(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: true);
        
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Sign-in failed.");
        }

        var identityUser = await _userManager.FindByNameAsync(request.Email)
            ?? throw new UserNotFoundException("User with provided username wasn't found!");

        await _signInManager.SignInAsync(identityUser, isPersistent: false);
        
        return identityUser;
    }

    public async Task<AuthResponseDto> LoginAccount(LoginDto request)
    {
        var identityUser = await SignInAndValidateUser(request);
        var response = await _jwtService.GenerateSecurityTokenAsync(identityUser);
        var baseUser = await _unitOfWork.Users.Find(x => x.IdentityId == identityUser.Id) 
                   ?? throw new UserNotFoundException("User with provided id wasn't found!");
        
        baseUser.RefreshToken = response.RefreshToken;
        baseUser.RefreshTokenExpiration = response.RefreshTokenExpiration;
        
        _unitOfWork.Users.Update(baseUser);
        await _unitOfWork.SaveChangesAsync();
        
        return response;
    }
    
    public async Task<AuthResponseDto> LoginStaff(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded) throw new InvalidOperationException("Sign-in failed.");
        
        var identityUser = await _userManager.FindByEmailAsync(request.Email) 
                   ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var baseUser = await _unitOfWork.Users.Find(x => x.IdentityId == identityUser.Id) 
                          ?? throw new UserNotFoundException("User with provided id wasn't found!");
            
        await _signInManager.SignInAsync(identityUser, isPersistent: false);
        var response = await _jwtService.GenerateSecurityTokenAsync(identityUser);
            
        baseUser.RefreshToken = response.RefreshToken;
        baseUser.RefreshTokenExpiration = response.RefreshTokenExpiration;
        
        _unitOfWork.Users.Update(baseUser);
        await _unitOfWork.SaveChangesAsync();
        
        return response;
    }

    private async Task<ApplicationUser> ValidateAndRetrieveUser(RefreshJwtDto request)
    {
        ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(request.Token)
            ?? throw new SecurityTokenException("Invalid JWT token");

        string? email = principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            throw new SecurityTokenException("Email claim not found in JWT token");
        }

        var identityUser = await _userManager.FindByEmailAsync(email)
                   ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var baseUser = await _unitOfWork.Users.Find(x => x.Id == identityUser.Id)
                       ?? throw new UserNotFoundException("User with provided id wasn't found!");
        
        if (baseUser == null || baseUser.RefreshToken != request.RefreshToken 
                             || baseUser.RefreshTokenExpiration <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        return identityUser;
    }

    public async Task<AuthResponseDto> RefreshToken(RefreshJwtDto request)
    {
        var identityUser = await ValidateAndRetrieveUser(request);
        var baseUser = await _unitOfWork.Users.Find(x => x.Id == identityUser.Id)
                       ?? throw new UserNotFoundException("User with provided id wasn't found!");
        
        var response = await _jwtService.GenerateSecurityTokenAsync(identityUser);
        baseUser.RefreshToken = response.RefreshToken;
        baseUser.RefreshTokenExpiration = response.RefreshTokenExpiration;

        _unitOfWork.Users.Update(baseUser);
        await _unitOfWork.SaveChangesAsync();
        
        return new AuthResponseDto
        {
            Token = response.Token,
            Email = identityUser.Email ?? throw new InvalidOperationException("Email cannot be null"),
            FirstName = (baseUser as UserProfile)?.FirstName,
            LastName = (baseUser as UserProfile)?.LastName,
            PhoneNumber = identityUser.PhoneNumber ?? throw new InvalidOperationException("Phone number cannot be null"),
            JwtExpiration = response.JwtExpiration,
            RefreshToken = response.RefreshToken,
            RefreshTokenExpiration = response.RefreshTokenExpiration,
            UserRole = response.UserRole
        };
    }

    public async Task<bool> ChangePassword(ChangePasswordDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) 
                   ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

        if (!validationResult.Succeeded)
        {
            return false;
        }
        
        var changeResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (!changeResult.Succeeded) return false;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }
    
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<bool> DeleteUser(string password)
    {
        var httpContext = _contextAccessor.HttpContext 
                          ?? throw new InvalidOperationException("HttpContext is null");
        var identityUser = await _userManager.GetUserAsync(httpContext.User) 
                           ?? throw new UserNotAuthorizedException("User is not Authorized");
        
        if (!await _userManager.CheckPasswordAsync(identityUser, password))
            throw new UserNotAuthorizedException("User not authorized!");
        
        await _userManager.DeleteAsync(identityUser);
        await _fileService.DeleteAllFilesAsync(identityUser.Id);
        
        return true;
    }

    public async Task<ApplicationUser> ChangeUserTypeToDefaultAccount(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        
        await _userManager.AddToRoleAsync(user, UserType.DefaultUser.ToString());
        await _userManager.UpdateAsync(user);

        return user;
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToBusinessAccount(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        
        await _userManager.AddToRoleAsync(user, UserType.BusinessUser.ToString());
        await _userManager.UpdateAsync(user);
        
        return user;
    }
}