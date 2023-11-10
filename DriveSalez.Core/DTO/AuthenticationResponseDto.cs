using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Core.DTO
{
    public class AuthenticationResponseDto
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
        
        public DateTime? JwtExpiration { get; set; }
        
        public DateTime? RefreshTokenExpiration { get; set; }
        
        public string? UserRole { get; set; }
    }
}
