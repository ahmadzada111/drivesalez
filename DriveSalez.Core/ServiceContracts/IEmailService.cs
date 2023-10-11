namespace DriveSalez.Core.ServiceContracts;

public interface IEmailService
{
    Task<bool> SendOtpByEmail(string toEmail, string otp);

    Task<bool> VerifyEmail(string email);
}