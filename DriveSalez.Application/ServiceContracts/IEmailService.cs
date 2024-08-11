using DriveSalez.Application.DTO;

namespace DriveSalez.Application.ServiceContracts;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailMetadata emailMetadata);    
}