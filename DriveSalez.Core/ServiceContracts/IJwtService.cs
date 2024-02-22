using DriveSalez.Core.DTO;
using System.Security.Claims;
using DriveSalez.Core.Domain.IdentityEntities;

namespace DriveSalez.Core.ServiceContracts;

public interface IJwtService
{
    Task<AuthenticationResponseDto> GenerateSecurityTokenAsync(ApplicationUser user);
    
    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
}
