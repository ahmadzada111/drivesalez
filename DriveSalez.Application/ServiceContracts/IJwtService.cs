using System.Security.Claims;
using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.ServiceContracts;

public interface IJwtService
{
    Task<AuthenticationResponseDto> GenerateSecurityTokenAsync(ApplicationUser user);
    
    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);
}
