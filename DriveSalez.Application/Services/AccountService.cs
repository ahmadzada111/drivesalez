using System.Security.Claims;
using AutoMapper;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BusinessAccount = DriveSalez.Domain.IdentityEntities.BusinessAccount;

namespace DriveSalez.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtService _jwtService;
    private readonly IAccountRepository _accountRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IHttpContextAccessor contextAccessor,
        IAccountRepository accountRepository, IFileService fileService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
        _jwtService = jwtService;
        _accountRepository = accountRepository;
        _fileService = fileService;
        _mapper = mapper;
    }

    private async Task<IdentityResult> RegisterUserAsync<TUser>(TUser user, string password, UserType userType) where TUser : ApplicationUser
    {
        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, userType.ToString());
            await _userManager.UpdateAsync(user);
            await _accountRepository.AddLimitsToAccountInDbAsync(user, userType);
        }

        return result;
    }

    public async Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request)
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false,
            CreationDate = DateTimeOffset.Now,
            SubscriptionExpirationDate = DateTimeOffset.Now.AddMonths(1),
            IsBanned = false
        };

        return await RegisterUserAsync(user, request.Password, UserType.DefaultAccount);
    }

    public async Task<IdentityResult> RegisterBusinessAccountAsync(RegisterBusinessAccountDto request)
    {
        BusinessAccount user = new BusinessAccount()
        {
            Email = request.Email,
            PhoneNumbers = _mapper.Map<List<PhoneNumber>>(request.PhoneNumbers),
            UserName = request.UserName,
            WorkHours = request.WorkHours,
            Address = request.Address,
            Description = request.Description,
            EmailConfirmed = false,
            CreationDate = DateTimeOffset.Now,
            SubscriptionExpirationDate = DateTimeOffset.Now,
            IsBanned = false
        };

        return await RegisterUserAsync(user, request.Password, UserType.BusinessAccount);
    }

    private async Task<TUser> SignInAndValidateUserAsync<TUser>(LoginDto request) where TUser : ApplicationUser
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Sign-in failed.");
        }

        var user = await _accountRepository.FindUserByLoginInDbAsync(request.UserName) as TUser
            ?? throw new UserNotFoundException("User with provided login wasn't found!");

        if (!user.EmailConfirmed)
        {
            throw new EmailNotConfirmedException("Email not confirmed!");
        }

        if (user.IsBanned)
        {
            throw new BannedUserException("User is banned!");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        
        return user;
    }

    public async Task<DefaultAccountAuthResponseDto?> LoginDefaultAccountAsync(LoginDto request)
    {
        var user = await SignInAndValidateUserAsync<DefaultAccount>(request);
        var response = await _jwtService.GenerateDefaultAccountSecurityTokenAsync(user);
        
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;
        
        await _userManager.UpdateAsync(user);
        
        return response;
    }

    public async Task<BusinessAccountAuthResponseDto?> LoginBusinessAccountAsync(LoginDto request)
    {
        var user = await SignInAndValidateUserAsync<BusinessAccount>(request);
        var response = await _jwtService.GenerateBusinessAccountSecurityTokenAsync(user);
        
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;
        
        await _userManager.UpdateAsync(user);
        
        return response;
    }

    public async Task<DefaultAccountAuthResponseDto?> LoginStaffAsync(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _accountRepository.FindUserByLoginInDbAsync(request.UserName) ?? throw new UserNotFoundException("User with provided login wasn't found!");
            await _signInManager.SignInAsync(user, isPersistent: false);
            var response = await _jwtService.GenerateDefaultAccountSecurityTokenAsync((DefaultAccount)user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiration = response.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return response;
        }
        
        return null;
    }

    private async Task<TUser> ValidateAndRetrieveUserAsync<TUser>(RefreshJwtDto request) where TUser : ApplicationUser
    {
        ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(request.Token)
            ?? throw new SecurityTokenException("Invalid JWT token");

        string? email = principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            throw new SecurityTokenException("Email claim not found in JWT token");
        }

        var user = await _userManager.FindByEmailAsync(email) as TUser;
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiration <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        return user;
    }

    public async Task<BusinessAccountAuthResponseDto> RefreshBusinessAccountAsync(RefreshJwtDto request)
    {
        var user = await ValidateAndRetrieveUserAsync<BusinessAccount>(request);

        var response = await _jwtService.GenerateBusinessAccountSecurityTokenAsync(user);
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;

        await _userManager.UpdateAsync(user);

        return new BusinessAccountAuthResponseDto
        {
            Token = response.Token,
            Email = user.Email,
            PhoneNumbers = _mapper.Map<List<string>>(user.PhoneNumbers),
            JwtExpiration = response.JwtExpiration,
            RefreshToken = response.RefreshToken,
            RefreshTokenExpiration = response.RefreshTokenExpiration,
            UserRole = response.UserRole
        };
    }   

    public async Task<DefaultAccountAuthResponseDto> RefreshDefaultAccountAsync(RefreshJwtDto request)
    {
        var user = await ValidateAndRetrieveUserAsync<DefaultAccount>(request);

        var response = await _jwtService.GenerateDefaultAccountSecurityTokenAsync(user);
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;

        await _userManager.UpdateAsync(user);

        return new DefaultAccountAuthResponseDto
        {
            Token = response.Token,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            JwtExpiration = response.JwtExpiration,
            RefreshToken = response.RefreshToken,
            RefreshTokenExpiration = response.RefreshTokenExpiration,
            UserRole = response.UserRole
        };
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

        if (!validationResult.Succeeded)
        {
            return false;
        }
        
        var changeResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        
        if (changeResult.Succeeded)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }
        }

        return false;
    }
    
    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var validationResult = await passwordValidator.ValidateAsync(_userManager, user, newPassword);

        if (!validationResult.Succeeded)
        {
            return false;
        }
        
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ChangeEmailAsync(string email, string newMail)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException("User with provided email wasn't found!");
        user.Email = newMail;
        user.UserName = newMail;
        
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }
        
        return false;
    }
    
    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<ApplicationUser> DeleteUserAsync(string password)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var currentUser = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not Authorized");
        
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == currentUser.Id.ToString());

        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var result = await _accountRepository.DeleteUserFromDbAsync(user);
            
            await _fileService.DeleteAllFilesAsync(user.Id);
            return result;
        }

        throw new UserNotAuthorizedException("User not authorized!");
    }

    public async Task<ApplicationUser> ChangeUserTypeToDefaultAccountAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        var defaultAccount = await _accountRepository.ChangeUserTypeToDefaultAccountInDbAsync(user);
        
        await _userManager.AddToRoleAsync(defaultAccount, UserType.DefaultAccount.ToString());
        await _userManager.UpdateAsync(defaultAccount);

        return defaultAccount;
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToBusinessAccountAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        var businessAccount = await _accountRepository.ChangeUserTypeToBusinessInDbAsync(user);
        
        await _userManager.AddToRoleAsync(businessAccount, UserType.BusinessAccount.ToString());
        await _userManager.UpdateAsync(businessAccount);
        
        return businessAccount;
    }
    
    public async Task<IdentityResult> CreateAdminAsync()
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = "admin",
            UserName = "admin",
            FirstName = "admin" ,
            LastName = "admin",
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, "Admin1234!");

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserType.Admin.ToString());
            
            return result;
        }

        return result;
    }
}