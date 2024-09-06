using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.Utilities;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Persistence.Services;

internal sealed class EmailService : IEmailService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFluentEmail _fluentEmail;

    public EmailService(UserManager<ApplicationUser> userManager, IFluentEmail fluentEmail)
    {
        _userManager = userManager;
        _fluentEmail = fluentEmail;
    }

    public async Task SendEmailAsync(EmailMetadata emailMetadata)
    {
        var user = await _userManager.FindByEmailAsync(emailMetadata.ToAddress) ??
        throw new UserNotFoundException("User with provided email wasn't found!");

        await _fluentEmail.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .Body(emailMetadata.Body, isHtml: emailMetadata.IsHtml)
            .SendAsync();
    }
}