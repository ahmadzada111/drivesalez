namespace DriveSalez.Core.DTO;

public class PaidUserAuthenticationResponseDto
{
    public string? Email { get; set; }
        
    public string? Token { get; set; }

    public string? RefreshToken { get; set; }
        
    public DateTime? JwtExpiration { get; set; }
        
    public DateTime? RefreshTokenExpiration { get; set; }
        
    public string? UserRole { get; set; }
}