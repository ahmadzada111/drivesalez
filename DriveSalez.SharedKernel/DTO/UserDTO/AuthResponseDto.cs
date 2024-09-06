using DateTime = System.DateTime;

namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record AuthResponseDto
{
    public string? FirstName { get; init; }
        
    public string? LastName { get; init; }
        
    public string Email { get; init; }
        
    public string? PhoneNumber { get; init; }
    
    public string? ProfilePhotoImageUrl { get; init; }
    
    public string Token { get; init; }

    public string RefreshToken { get; init; }
        
    public DateTime JwtExpiration { get; init; }
        
    public DateTime RefreshTokenExpiration { get; init; }
        
    public string UserRole { get; init; }
}