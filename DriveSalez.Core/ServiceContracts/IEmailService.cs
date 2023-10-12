using DriveSalez.Core.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Core.ServiceContracts;

public interface IEmailService
{
    Task<bool> SendOtpByEmail(string toEmail, string otp);

    Task<bool> VerifyEmail(string email);

    Task<bool> ResetPassword(ResetPasswordDto request);
}