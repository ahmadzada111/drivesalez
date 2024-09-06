namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record LoginDto
{
    public string Email { get; init; }
    
    public string Password { get; init; }
}