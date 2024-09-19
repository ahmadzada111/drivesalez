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
    private readonly ILimitService _limitService;
    private readonly IWorkHourService _workHourService;
    
    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        IJwtService jwtService, IHttpContextAccessor contextAccessor, 
        IFileService fileService, IMapper mapper, IUnitOfWork unitOfWork, 
        ILimitService limitService, IWorkHourService workHourService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
        _jwtService = jwtService;
        _fileService = fileService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _limitService = limitService;
        _workHourService = workHourService;
    }

    public async Task<ApplicationUser> FindByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new UserNotFoundException("User not found");
        
        return user;
    }

    public async Task<ApplicationUser> FindByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName)
                   ?? throw new UserNotFoundException("User not found");
        
        return user;
    }
    
    public async Task<IdentityResult> ChangeEmail(ApplicationUser identityUser, string newEmail, string token)
    {
        var result = await _userManager.ChangeEmailAsync(identityUser, newEmail, token);

        if (result.Succeeded)
        {
            identityUser.UserName = newEmail;
            await _userManager.UpdateAsync(identityUser);
        }

        return result;
    }
    
    public Task<string> GenerateEmailConfirmationToken(ApplicationUser identityUser)
    {
        return _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
    }

    public Task<string> GeneratePasswordResetToken(ApplicationUser identityUser)
    {
        return _userManager.GeneratePasswordResetTokenAsync(identityUser);
    }

    public Task<string> GenerateChangeEmailToken(ApplicationUser identityUser, string newEmail)
    {
        return _userManager.GenerateChangeEmailTokenAsync(identityUser, newEmail);
    }

    public async Task<ApplicationUser> FindById(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString())
                   ?? throw new UserNotFoundException("User not found.");
    }
    
    public async Task<IdentityResult> ConfirmEmail(ApplicationUser identityUser, string token)
    {
        return await _userManager.ConfirmEmailAsync(identityUser, token);
    }

    public async Task<IdentityResult> ResetPassword(ApplicationUser identityUser, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(identityUser, token, newPassword);
    }

    public async Task<IdentityResult> RegisterAccount(RegisterAccountDto request, FileUploadData? profilePhotoData, 
        FileUploadData? backgroundPhotoData, UserType userType)
    {
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            AccountBalance = 0,
            CreationDate = DateTimeOffset.UtcNow,
            PhoneNumbers = _mapper.Map<List<PhoneNumber>>(request.PhoneNumbers),
            Address = request.Address,
            Description = request.Description,
            BusinessName = request.BusinessName
        };

        if (profilePhotoData is not null) await HandleProfilePhotoUpload(profilePhotoData, user);
        if (backgroundPhotoData is not null) await HandleBackgroundPhotoUpload(backgroundPhotoData, user);
        if (request.WorkHours is not null) _workHourService.AddWorkHoursToUser(user, request.WorkHours);
        
        var identityUser =  new ApplicationUser()
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Email,
            EmailConfirmed = false,
            BaseUser = user
        };

        IdentityResult result = await _userManager.CreateAsync(identityUser, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(identityUser, userType.ToString());
            await _userManager.UpdateAsync(identityUser);
            await _limitService.AddLimitToUser(user, userType);
            await _unitOfWork.SaveChangesAsync();
        }
        
        return result;
    }
    
    private async Task HandleBackgroundPhotoUpload(FileUploadData fileUploadData, User user)
    {
        var backgroundPhotoUrl = await _fileService.UploadFilesAsync(new List<FileUploadData> { fileUploadData }, user);
        user.BackgroundPhotoUrl = backgroundPhotoUrl.FirstOrDefault();
        _unitOfWork.Users.Update(user);
    }

    private async Task HandleProfilePhotoUpload(FileUploadData fileUploadData, User user)
    {
        var profilePhotoUrl = await _fileService.UploadFilesAsync(new List<FileUploadData> { fileUploadData }, user);
        user.ProfilePhotoUrl = profilePhotoUrl.FirstOrDefault();
        _unitOfWork.Users.Update(user);
    }
    
    private async Task<ApplicationUser> SignInAndValidateUser(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: true);
        
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Sign-in failed.");
        }

        var identityUser = await _userManager.FindByNameAsync(request.UserName)
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

    public async Task<ApplicationUser> FindAuthorizedUser()
    {
        var httpContext = _contextAccessor.HttpContext
                          ?? throw new NullReferenceException("HttpContext is null");
                          
        return await _userManager.GetUserAsync(httpContext.User) 
                           ?? throw new UserNotAuthorizedException("User is not authorized!");
    }
    
    public async Task<UserProfileDto> FindUserProfile(Guid userId, ApplicationUser identityUser)
    {
        var user = await _unitOfWork.Users.FindUserOfType<User>(x => x.Id == userId, 
                       x => x.PhoneNumbers,
                       x => x.WorkHours)
            ?? throw new UserNotFoundException("User with provided id wasn't found!");
        
        var userRole = await _userManager.GetRolesAsync(identityUser);
        var profilePhotoUrl = await _unitOfWork.ImageUrls.Find(x => x.ProfilePhotoUserId == user.Id);
        var backgroundPhotoUrl = await _unitOfWork.ImageUrls.Find(x => x.BackgroundPhotoUserId == user.Id);
        
        return new UserProfileDto()
        {
            Id = user.Id,
            Email = identityUser.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = identityUser.PhoneNumber,
            PhoneNumbers = _mapper.Map<List<string>>(user.PhoneNumbers),
            ProfilePhotoImageUrl = profilePhotoUrl?.Url.ToString(),
            BackgroundPhotoImageUrl = backgroundPhotoUrl?.Url.ToString(),
            UserRole = userRole.FirstOrDefault()!,
            WorkHours = _mapper.Map<List<WorkHourDto>>(user.WorkHours)
        };
    }
    
    private async Task<ApplicationUser> ValidateAndRetrieveUser(RefreshJwtDto request)
    {
        ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(request.Token);

        string? email = principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            throw new SecurityTokenException("Email claim not found in JWT token");
        }

        var identityUser = await _userManager.FindByEmailAsync(email)
                   ?? throw new UserNotFoundException("User with provided email wasn't found!");
        var baseUser = await _unitOfWork.Users.Find(x => x.Id == identityUser.Id)
                       ?? throw new UserNotFoundException("User with provided id wasn't found!");
        
        if (baseUser.RefreshToken != request.RefreshToken 
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
        
        return response;
    }

    public async Task<bool> ChangePassword(ChangePasswordDto request)
    {
        var user = await FindByEmail(request.Email);
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
    
    public async Task<IdentityResult> DeleteUser(ApplicationUser identityUser, string password)
    {
        if (!await _userManager.CheckPasswordAsync(identityUser, password))
            throw new UserNotAuthorizedException("User not authorized!");
        
        var result = await _userManager.DeleteAsync(identityUser);
        await _fileService.DeleteAllFilesAsync(identityUser.Id);
        
        return result;
    }

    public async Task<ApplicationUser> ChangeUserRole(ApplicationUser identityUser, UserType userType)
    {
        var roles = await _userManager.GetRolesAsync(identityUser);
        await _userManager.RemoveFromRolesAsync(identityUser, roles);
        
        await _userManager.AddToRoleAsync(identityUser, userType.ToString());
        await _userManager.UpdateAsync(identityUser);

        return identityUser;
    }

   public async Task ChangeProfilePhoto(FileUploadData fileUploadData, ApplicationUser identityUser)
    {
        var user = await _unitOfWork.Users.FindUserOfType<User>(x => x.Id == identityUser.Id) 
                   ?? throw new UserNotFoundException("User with the provided id wasn't found!");

        var existingProfilePhoto = await _unitOfWork.ImageUrls.Find(x => x.ProfilePhotoUserId == user.Id);
        var uploadedUrls = await _fileService.UploadFilesAsync(new List<FileUploadData> { fileUploadData }, user);

        if (!uploadedUrls.Any())
        {
            throw new InvalidOperationException("Failed to upload the profile photo.");
        }

        var newProfilePhotoUrl = uploadedUrls.First().Url;

        if (existingProfilePhoto == null)
        {
            var newProfilePhoto = new ImageUrl
            {
                ProfilePhotoUserId = user.Id,
                Url = newProfilePhotoUrl
            };
            _unitOfWork.ImageUrls.Add(newProfilePhoto);
        }
        else
        {
            existingProfilePhoto.Url = newProfilePhotoUrl;
            _unitOfWork.ImageUrls.Update(existingProfilePhoto);
        }

        await _unitOfWork.SaveChangesAsync();
    }
   
    public async Task ChangeBackgroundPhoto(FileUploadData fileUploadData, ApplicationUser identityUser)
    {
        var user = await _unitOfWork.Users.FindUserOfType<User>(x => x.Id == identityUser.Id) 
                   ?? throw new UserNotFoundException("User with the provided id wasn't found!");

        var existingBackgroundPhoto = await _unitOfWork.ImageUrls.Find(x => x.BackgroundPhotoUserId == user.Id);
        var uploadedUrls = await _fileService.UploadFilesAsync(new List<FileUploadData> { fileUploadData }, user);

        if (!uploadedUrls.Any())
        {
            throw new InvalidOperationException("Failed to upload the background photo.");
        }

        var newBackgroundPhotoUrl = uploadedUrls.First().Url;

        if (existingBackgroundPhoto == null)
        {
            var newBackgroundPhoto = new ImageUrl
            {
                ProfilePhotoUserId = user.Id,
                Url = newBackgroundPhotoUrl
            };
            _unitOfWork.ImageUrls.Add(newBackgroundPhoto);
        }
        else
        {
            existingBackgroundPhoto.Url = newBackgroundPhotoUrl;
            _unitOfWork.ImageUrls.Update(existingBackgroundPhoto);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}