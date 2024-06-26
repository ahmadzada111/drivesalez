using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request);

    Task<IdentityResult> RegisterPremiumAccountAsync(RegisterPaidAccountDto request);

    Task<IdentityResult> RegisterBusinessAccountAsync(RegisterPaidAccountDto request);

    Task<AuthenticationResponseDto?> LoginAsync(LoginDto request);

    Task<AuthenticationResponseDto?> LoginStaffAsync(LoginDto request);
    
    Task<AuthenticationResponseDto> RefreshAsync(RefreshJwtDto request);

    Task<ApplicationUser> DeleteUserAsync(string password);
    
    Task<bool> ChangePasswordAsync(ChangePasswordDto request);

    Task<bool> ResetPasswordAsync(string email, string newPassword);

    Task LogOutAsync();

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToPremiumAccountAsync(ApplicationUser user);

    Task<ApplicationUser> ChangeUserTypeToBusinessAccountAsync(ApplicationUser user);

    Task<bool> ChangeEmailAsync(string email, string newMail);
    
    Task<IdentityResult>  CreateAdminAsync();
}