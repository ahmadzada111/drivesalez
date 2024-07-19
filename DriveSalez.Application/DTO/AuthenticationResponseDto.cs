using DriveSalez.Domain.Entities;
using DateTime = System.DateTime;

namespace DriveSalez.Application.DTO
{
    public class AuthenticationResponseDto
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        
        public List<AccountPhoneNumber> PhoneNumbers { get; set; }
        
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
        
        public DateTime? JwtExpiration { get; set; }
        
        public DateTime? RefreshTokenExpiration { get; set; }
        
        public string? UserRole { get; set; }
    }
}
