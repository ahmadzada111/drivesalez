namespace DriveSalez.SharedKernel.DTO;

public record ConfirmEmailDto
{
    public Guid UserId { get; init; }
    
    public string Token { get; init; }
}
