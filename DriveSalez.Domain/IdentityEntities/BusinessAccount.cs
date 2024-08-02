using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.IdentityEntities;

public class BusinessAccount : ApplicationUser
{
    public ImageUrl? ProfilePhotoUrl { get; set; } 

    public ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public string? Address { get; set; }

    public string Description { get; set; }
    
    public string WorkHours { get; set; }
}