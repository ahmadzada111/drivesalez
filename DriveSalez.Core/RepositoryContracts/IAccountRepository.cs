using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.RepositoryContracts;

public interface IAccountRepository
{
    public Task<AuthenticationResponseDto> SendRegistrationDataToDb(ApplicationUser user, string password);
    
    public Task<AuthenticationResponseDto> SendLoginDataToDb(LoginDto request);
    
    public Task<AuthenticationResponseDto> SendRefreshTokenDataToDb(RefreshJwtDto request);
}