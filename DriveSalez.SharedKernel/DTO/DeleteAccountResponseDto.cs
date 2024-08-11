namespace DriveSalez.Application.DTO;

public record DeleteAccountResponseDto
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
}