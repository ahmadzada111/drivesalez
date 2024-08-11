namespace DriveSalez.Application.DTO;

public record GetModeratorDto
{
    public Guid Id { get; init; }
    
    public string Email { get; init; }
    
    public string Name { get; init; }
    
    public string Surname { get; init; }
}