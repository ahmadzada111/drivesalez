using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities;

public class PremiumAccount : ApplicationUser
{
    public List<CarDealerPhoneNumber> PhoneNumbers { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }
    
    public string? WorkHours { get; set; }
}