using DriveSalez.Core.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Core.ServiceContracts;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    
    Task<bool> VerifyEmailAsync(string email);
}