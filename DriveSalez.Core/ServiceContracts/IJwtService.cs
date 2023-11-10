using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using System.Security.Claims;
using DriveSalez.Core.Entities;


namespace DriveSalez.Core.ServiceContracts;

public interface IJwtService
{
    Task<AuthenticationResponseDto> GenerateSecurityTokenAsync(ApplicationUser user);
    
    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
}
