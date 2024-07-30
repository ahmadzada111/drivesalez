using DriveSalez.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Domain.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
        
    public string? LastName { get; set; }
        
    public string? RefreshToken { get; set; }
        
    public DateTime? RefreshTokenExpiration { get; set; }
        
    public List<Announcement>? Announcements { get; set; } = new List<Announcement>();
        
    public List<PhoneNumber>? PhoneNumbers { get; set; }

    public DateTimeOffset SubscriptionExpirationDate { get; set; }

    public int PremiumUploadLimit { get; set; }

    public int RegularUploadLimit { get; set; }
        
    public decimal AccountBalance { get; set; }
        
    public DateTimeOffset CreationDate { get; set; }
        
    public DateTimeOffset LastUpdateDate { get; set; }
        
    public bool IsBanned { get; set; }
}