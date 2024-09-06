using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.IdentityEntities;

public class UserProfile : BaseUser
{
    public ICollection<Announcement> Announcements { get; set; } = [];
    
    public ICollection<UserLimit> UserLimits { get; set; } = [];

    public ICollection<UserPurchase> UserPurchases { get; set; } = [];
    
    public ICollection<PhoneNumber>? PhoneNumbers { get; set; } = [];
    
    public UserSubscription Subscription { get; }
    
    public decimal AccountBalance { get; set; }
    
    public ImageUrl? ProfilePhotoUrl { get; set; } 

    public ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public string? Address { get; set; }

    public string? Description { get; set; }
    
    public string? WorkHours { get; set; }
}