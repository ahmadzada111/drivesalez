using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserService
{
    Task<IdentityResult> RegisterAccount(RegisterAccountDto request, FileUploadData? profilePhotoData, 
        FileUploadData? backgroundPhotoData, UserType userType);

    Task<ApplicationUser> FindByEmail(string email);

    Task<ApplicationUser> FindAuthorizedUser();
    
    Task<ApplicationUser> FindById(Guid userId);
    
    Task<string> GenerateEmailConfirmationToken(ApplicationUser identityUser);

    Task<string> GeneratePasswordResetToken(ApplicationUser identityUser);
    
    Task<string> GenerateChangeEmailToken(ApplicationUser identityUser, string newEmail);
    
    Task<IdentityResult> ConfirmEmail(ApplicationUser identityUser, string token);
    
    Task<IdentityResult> ResetPassword(ApplicationUser identityUser, string token, string newPassword);

    Task<IdentityResult> ChangeEmail(ApplicationUser identityUser, string newEmail, string token);
    
    Task<AuthResponseDto> LoginAccount(LoginDto request);
    
    Task<UserProfileDto> FindUserProfile(Guid userId, ApplicationUser identityUser);
    
    Task<AuthResponseDto> RefreshToken(RefreshJwtDto request);

    Task<IdentityResult> DeleteUser(ApplicationUser identityUser, string password);
    
    Task<bool> ChangePassword(ChangePasswordDto request);
    
    Task LogOut();

    Task ChangeProfilePhoto(FileUploadData fileUploadData, ApplicationUser identityUser);

    Task ChangeBackgroundPhoto(FileUploadData fileUploadData, ApplicationUser identityUser);
    
    Task<ApplicationUser> ChangeUserRole(ApplicationUser identityUser, UserType userType);
}