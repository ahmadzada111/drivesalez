using DriveSalez.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Domain.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{       
    public virtual string FirstName { get; set; }
        
    public virtual string LastName { get; set; }
    
    public virtual List<PhoneNumber> PhoneNumbers { get; set; }
    
    public virtual ImageUrl? ProfilePhotoUrl { get; set; } 

    public virtual ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public virtual string? Address { get; set; }

    public virtual string Description { get; set; }
    
    public virtual string WorkHours { get; set; }

    public string? RefreshToken { get; set; }
        
    public DateTime? RefreshTokenExpiration { get; set; }
        
    public List<Announcement>? Announcements { get; set; } = new List<Announcement>();
        
    public DateTimeOffset SubscriptionExpirationDate { get; set; }

    public int PremiumUploadLimit { get; set; }

    public int RegularUploadLimit { get; set; }
        
    public decimal AccountBalance { get; set; }
        
    public DateTimeOffset CreationDate { get; set; }
        
    public DateTimeOffset? LastUpdateDate { get; set; }
        
    public bool IsBanned { get; set; }
}