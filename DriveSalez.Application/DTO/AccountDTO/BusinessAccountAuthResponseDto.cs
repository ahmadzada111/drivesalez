using DateTime = System.DateTime;

namespace DriveSalez.Application.DTO.AccountDTO;

public record BusinessAccountAuthResponseDto
{       
    public string Email { get; init; }
        
    public List<string> PhoneNumbers { get; init; }
        
    public string Token { get; init; }

    public string RefreshToken { get; init; }
        
    public DateTime JwtExpiration { get; init; }
        
    public DateTime RefreshTokenExpiration { get; init; }
        
    public string UserRole { get; init; }
}