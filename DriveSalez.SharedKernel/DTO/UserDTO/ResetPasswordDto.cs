namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record ResetPasswordDto
{
    public string Email { get; init; }
    
    public string Token { get; init; }
        
    public string NewPassword { get; init; }
}