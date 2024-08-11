namespace DriveSalez.Application.DTO;

public record GetUserDto
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
    
    public bool IsBanned { get; init; }
    
    public List<string> PhoneNumbers { get; init; }
}