using DriveSalez.Domain.Entities;
using DateTime = System.DateTime;

namespace DriveSalez.Application.DTO.AccountDTO;

public record AuthenticationResponseDto
{
    public string? FirstName { get; init; }
        
    public string? LastName { get; init; }
        
    public string? Email { get; init; }
        
    public List<string> PhoneNumbers { get; init; }
        
    public string? Token { get; init; }

    public string? RefreshToken { get; init; }
        
    public DateTime? JwtExpiration { get; init; }
        
    public DateTime? RefreshTokenExpiration { get; init; }
        
    public string? UserRole { get; init; }
}