using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterDto request);

    Task<AuthenticationResponseDto> LoginAsync(LoginDto request);

    Task<AuthenticationResponseDto> RefreshAsync(RefreshJwtDto request);

    Task<DeleteAccountResponseDto> DeleteUserAsync(string password);
    
    Task<bool> ChangePasswordAsync(ChangePasswordDto request);
}