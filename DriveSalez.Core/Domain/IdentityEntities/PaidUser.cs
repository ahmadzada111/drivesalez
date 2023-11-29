using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities;

public class PaidUser : ApplicationUser
{
    public ImageUrl? ProfilePhotoUrl { get; set; } 

    public ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public DateTimeOffset SubscriptionExpirationDate { get; set; }
    
    public string? Address { get; set; }

    public string? Description { get; set; }
    
    public string? WorkHours { get; set; }
}