namespace DriveSalez.Application.DTO;

public record ChangeEmailDto
{
    public ValidateOtpDto ValidateRequest { get; init; }
    
    public string NewMail { get; init; }
}