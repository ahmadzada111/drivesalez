namespace DriveSalez.Application.DTO.AccountDTO;

public record DeleteAccountResponseDto
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
}