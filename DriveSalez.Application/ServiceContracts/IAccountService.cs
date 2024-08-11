using DriveSalez.Application.DTO;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request);

    Task<IdentityResult> RegisterBusinessAccountAsync(RegisterBusinessAccountDto request);

    Task<DefaultAccountAuthResponseDto?> LoginDefaultAccountAsync(LoginDto request);

    Task<BusinessAccountAuthResponseDto?> LoginBusinessAccountAsync(LoginDto request);

    Task<DefaultAccountAuthResponseDto?> LoginStaffAsync(LoginDto request);

    Task<BusinessAccountAuthResponseDto> RefreshBusinessAccountAsync(RefreshJwtDto request);

    Task<DefaultAccountAuthResponseDto> RefreshDefaultAccountAsync(RefreshJwtDto request);

    Task<ApplicationUser> DeleteUserAsync(string password);
    
    Task<bool> ChangePasswordAsync(ChangePasswordDto request);

    Task<bool> ResetPasswordAsync(string email, string newPassword);

    Task LogOutAsync();

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToBusinessAccountAsync(ApplicationUser user);

    Task<bool> ChangeEmailAsync(string email, string newMail);

    Task<bool> VerifyEmailAsync(string email);
}