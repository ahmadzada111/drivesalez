using System.Security.Claims;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO.UserDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IJwtService
{   
    ClaimsPrincipal GetPrincipalFromJwtToken(string token);

    Task<AuthResponseDto> GenerateSecurityTokenAsync(ApplicationUser identityUser);
}
