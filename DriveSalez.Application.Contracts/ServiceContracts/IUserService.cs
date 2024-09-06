using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserService
{
    Task<IdentityResult> RegisterAccount(RegisterAccountDto request, UserType userType);

    Task<ApplicationUser> FindByEmail(string email);
    
    Task<string> GenerateEmailConfirmationToken(ApplicationUser user);

    Task<string> GeneratePasswordResetToken(ApplicationUser user);
    
    Task<string> GenerateChangeEmailToken(ApplicationUser user, string newEmail);
    
    Task<IdentityResult> ConfirmEmail(Guid userId, string token);
    
    Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string newPassword);

    Task<IdentityResult> ChangeEmail(Guid userId, string newEmail, string token);
    
    Task<AuthResponseDto> LoginAccount(LoginDto request);
    
    Task<AuthResponseDto> LoginStaff(LoginDto request);
    
    Task<AuthResponseDto> RefreshToken(RefreshJwtDto request);

    Task<bool> DeleteUser(string password);
    
    Task<bool> ChangePassword(ChangePasswordDto request);
    
    Task LogOut();

    Task<ApplicationUser> ChangeUserTypeToDefaultAccount(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToBusinessAccount(ApplicationUser user);
}