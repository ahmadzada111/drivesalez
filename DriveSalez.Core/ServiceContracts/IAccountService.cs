using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request);

    Task<IdentityResult> RegisterPremiumAccountAsync(RegisterPaidAccountDto request);

    Task<IdentityResult> RegisterBusinessAccountAsync(RegisterPaidAccountDto request);

    Task<AuthenticationResponseDto> LoginAsync(LoginDto request);
    
    Task<AuthenticationResponseDto> RefreshAsync(RefreshJwtDto request);

    Task<bool> DeleteUserAsync(string password);
    
    Task<bool> ChangePasswordAsync(ChangePasswordDto request);

    Task<bool> ResetPasswordAsync(string email, string newPassword);

    Task LogOutAsync();

    Task<ApplicationUser> ChangeUserTypeToDefaultAccountAsync(ApplicationUser user);
}