namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record ChangeEmailDto
{
    public string Email { get; init; }
    
    public string NewEmail { get; init; }
}