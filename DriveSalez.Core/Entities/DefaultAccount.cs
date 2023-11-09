using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.Entities;

public class DefaultAccount : ApplicationUser
{
    public string? FirstName { get; set; }
        
    public string? LastName { get; set; }
}