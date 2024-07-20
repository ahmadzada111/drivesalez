namespace DriveSalez.Application.DTO.AccountDTO;

public record ChangeEmailDto
{
    public ValidateOtpDto ValidateRequest { get; init; }
    
    public string NewMail { get; init; }
}