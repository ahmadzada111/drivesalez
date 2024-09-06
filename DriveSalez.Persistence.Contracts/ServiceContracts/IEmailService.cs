using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Persistence.Contracts.ServiceContracts;

public interface IEmailService
{
    Task SendEmailAsync(EmailMetadata emailMetadata);    
}