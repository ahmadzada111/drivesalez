using DriveSalez.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DriveSalez.Core.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public string? RefreshToken { get; set; }
        
        public List<AccountPhoneNumber>? PhoneNumbers { get; set; }
        
        public DateTime? RefreshTokenExpiration { get; set; }
        
        public List<Announcement>? Announcements { get; set; } = new List<Announcement>();
        
        public int PremiumUploadLimit { get; set; }

        public int RegularUploadLimit { get; set; }
        
        public decimal AccountBalance { get; set; }
        
        public DateTimeOffset CreationDate { get; set; }
        
        public DateTimeOffset LastUpdateDate { get; set; }
    }
}
