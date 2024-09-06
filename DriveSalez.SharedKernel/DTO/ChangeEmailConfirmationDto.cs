namespace DriveSalez.SharedKernel.DTO;

public record ChangeEmailConfirmationDto
{
    public Guid UserId { get; init; }
    
    public string NewEmail { get; init; }
    
    public string Token { get; init; }
}