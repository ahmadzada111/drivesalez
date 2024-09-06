namespace DriveSalez.SharedKernel.DTO;

public record RegisterModeratorResponseDto
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
}