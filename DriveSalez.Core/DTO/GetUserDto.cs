using DriveSalez.Core.Domain.Entities;

namespace DriveSalez.Core.DTO;

public class GetUserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public bool IsBanned { get; set; }
    
    public List<AccountPhoneNumber> PhoneNumbers { get; set; }
}