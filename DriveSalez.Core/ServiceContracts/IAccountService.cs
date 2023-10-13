using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    Task<AuthenticationResponseDto> Register(RegisterDto request);

    Task<AuthenticationResponseDto> Login(LoginDto request);

    Task<AuthenticationResponseDto> Refresh(RefreshJwtDto request);

    Task<ApplicationUser> DeleteUser(string password);
}