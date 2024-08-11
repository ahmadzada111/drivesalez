using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.IdentityEntities;

public class BusinessAccount : ApplicationUser
{
    public override ImageUrl? ProfilePhotoUrl { get; set; } 

    public override ImageUrl? BackgroundPhotoUrl { get; set; }
    
    public override string? Address { get; set; }

    public override string Description { get; set; }
    
    public override string WorkHours { get; set; }
}