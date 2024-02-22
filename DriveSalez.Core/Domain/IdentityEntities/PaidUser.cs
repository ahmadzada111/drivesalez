using DriveSalez.Core.Domain.Entities;

namespace DriveSalez.Core.Domain.IdentityEntities;

public class PaidUser : ApplicationUser
{
    public ImageUrl? ProfilePhotoUrl { get; set; } 

    public ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public string? Address { get; set; }

    public string? Description { get; set; }
    
    public string? WorkHours { get; set; }
}