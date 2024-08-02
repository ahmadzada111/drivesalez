using System.Security.Claims;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.ServiceContracts;

public interface IJwtService
{   
    ClaimsPrincipal? GetPrincipalFromJwtToken(string token);

    Task<DefaultAccountAuthResponseDto> GenerateDefaultAccountSecurityTokenAsync(DefaultAccount user);

    Task<BusinessAccountAuthResponseDto> GenerateBusinessAccountSecurityTokenAsync(BusinessAccount user);
}
