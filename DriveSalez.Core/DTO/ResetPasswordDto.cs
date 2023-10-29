namespace DriveSalez.Core.DTO;

public class ResetPasswordDto
{
    public ValidateOtpDto ValidateRequest { get; set; }
    
    public string NewPassword { get; set; }
}