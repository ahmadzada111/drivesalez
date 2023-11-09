using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request);

    Task<IdentityResult> RegisterPremiumAccountAsync(RegisterPaidAccountDto request);

    Task<IdentityResult> RegisterBusinessAccountAsync(RegisterPaidAccountDto request);

    Task<DefaultUserAuthenticationResponseDto> LoginDefaultAccountAsync(LoginDto request);

    Task<PaidUserAuthenticationResponseDto> LoginPaidAccountAsync(LoginDto request);
    
    Task<DefaultUserAuthenticationResponseDto> RefreshDefaultAccountAsync(RefreshJwtDto request);

    Task<PaidUserAuthenticationResponseDto> RefreshPaidAccountAsync(RefreshJwtDto request);

    Task<bool> DeleteUserAsync(string password);
    
    Task<bool> ChangePasswordAsync(ChangePasswordDto request);

    Task<bool> ResetPasswordAsync(string email, string newPassword);

    Task LogOutAsync();
}