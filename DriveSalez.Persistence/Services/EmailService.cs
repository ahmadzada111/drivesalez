using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Persistence.Services;

public class EmailService : IEmailService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFluentEmail _fluentEmail;

    public EmailService(UserManager<ApplicationUser> userManager, IFluentEmail fluentEmail)
    {
        _userManager = userManager;
        _fluentEmail = fluentEmail;
    }

    public async Task<bool> SendEmailAsync(EmailMetadata emailMetadata)
    {
        var user = await _userManager.FindByEmailAsync(emailMetadata.ToAddress) ??
        throw new UserNotFoundException("User with provided email wasn't found!");

        await _fluentEmail.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .Body(emailMetadata.Body, isHtml: emailMetadata.IsHtml)
            .SendAsync();

        return true;
    }
}