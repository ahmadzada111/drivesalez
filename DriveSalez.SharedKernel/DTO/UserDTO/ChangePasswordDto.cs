namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record ChangePasswordDto
{
    public string Email { get; init; }
    
    public string OldPassword { get; init; }
    
    public string NewPassword { get; init; }
    
    public string ConfirmPassword { get; init; }
}