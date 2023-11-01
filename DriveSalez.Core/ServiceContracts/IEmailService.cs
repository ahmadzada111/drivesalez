using DriveSalez.Core.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Core.ServiceContracts;

public interface IEmailService
{
    Task<bool> SendOtpByEmailAsync(string toEmail, string otp);

    Task<bool> VerifyEmailAsync(string email);
}