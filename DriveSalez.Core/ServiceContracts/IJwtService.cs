using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using System.Security.Claims;
using DriveSalez.Core.Entities;


namespace DriveSalez.Core.ServiceContracts;

public interface IJwtService
{
    Task<DefaultUserAuthenticationResponseDto> GenerateSecurityTokenForDefaultUserAsync(DefaultAccount user);

    Task<PaidUserAuthenticationResponseDto> GenerateSecurityTokenForPaidUserAsync(PaidUser user);
    
    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
}
