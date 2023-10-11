using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using System.Security.Claims;


namespace DriveSalez.Core.ServiceContracts;

public interface IJwtService
{
    Task<AuthenticationResponseDto> GenerateSecurityToken(ApplicationUser user);

    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
}
