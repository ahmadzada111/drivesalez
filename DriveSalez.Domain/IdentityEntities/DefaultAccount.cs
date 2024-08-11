namespace DriveSalez.Domain.IdentityEntities;

public class DefaultAccount : ApplicationUser
{
    public override string FirstName { get; set; }
        
    public override string LastName { get; set; }
}