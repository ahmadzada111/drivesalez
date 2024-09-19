using DateTime = System.DateTime;

namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record AuthResponseDto
{
    public Guid Id { get; init; }
    
    public string Token { get; init; }

    public string RefreshToken { get; init; }
        
    public DateTime JwtExpiration { get; init; }
        
    public DateTime RefreshTokenExpiration { get; init; }
}