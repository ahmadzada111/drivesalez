using System.Net;
using System.Net.Mail;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Settings;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailService(EmailSettings emailSettings, UserManager<ApplicationUser> userManager)
    {
        _emailSettings = emailSettings;
        _userManager = userManager;
    }
    
    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        var user = await _userManager.FindByEmailAsync(toEmail);
        
        if (user == null)
        {
            throw new UserNotFoundException("User with provided email wasn't found!");
        }
        
        var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port);
        client.EnableSsl = true;
        
        client.Credentials = new NetworkCredential(
            _emailSettings.CompanyEmail,
            _emailSettings.EmailKey);
    
        var message = new MailMessage()
        {
            Subject = subject,        
            Body = body
        };

        // message.IsBodyHtml = true;
        message.From = new MailAddress(_emailSettings.CompanyEmail, _emailSettings.SenderName);
        message.To.Add(new MailAddress(toEmail));

        await client.SendMailAsync(message);

        return true;
    }
    
    public async Task<bool> VerifyEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User with provided email wasn't found!");
        }
        
        user.EmailConfirmed = true;
        
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
}