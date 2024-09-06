namespace DriveSalez.SharedKernel.DTO;

public record RegisterModeratorDto
{
    public string Email { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Password { get; init; }
}