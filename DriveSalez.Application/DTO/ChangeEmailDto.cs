namespace DriveSalez.Application.DTO;

public class ChangeEmailDto
{
    public ValidateOtpDto ValidateRequest { get; set; }
    
    public string NewMail { get; set; }
}