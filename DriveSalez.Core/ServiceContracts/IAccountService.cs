using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<IdentityResult> Register(RegisterDto request);

    Task<AuthenticationResponseDto> Login(LoginDto request);

    Task<AuthenticationResponseDto> Refresh(RefreshJwtDto request);

    Task<ApplicationUser> DeleteUser(string password);
    
    Task<bool> ChangePassword(ChangePasswordDto request);
}