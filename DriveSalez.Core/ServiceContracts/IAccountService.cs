using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IAccountService
{
    public Task<AuthenticationResponseDto> Register(RegisterDto request);

    public Task<AuthenticationResponseDto> Login(LoginDto request);

    public Task<AuthenticationResponseDto> Refresh(RefreshJwtDto request);
}