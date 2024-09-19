namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record LoginDto
{
    public string UserName { get; init; }
    
    public string Password { get; init; }
}