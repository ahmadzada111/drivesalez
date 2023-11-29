using System.Net;
using System.Net.Mail;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _emailConfig;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailService(IConfiguration emailConfig, UserManager<ApplicationUser> userManager)
    {
        _emailConfig = emailConfig;
        _userManager = userManager;
    }
    
    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        var user = await _userManager.FindByEmailAsync(toEmail);
        
        if (user == null)
        {
            throw new UserNotFoundException("User is not found!");
        }
        
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        
        client.Credentials = new NetworkCredential(
            _emailConfig["Email:CompanyEmail"],
            _emailConfig["Email:EmailKey"]);
    
        var message = new MailMessage()
        {
            Subject = subject,        
            Body = body
        };

        message.IsBodyHtml = true;
        message.From = new MailAddress(_emailConfig["Email:CompanyEmail"], "DriveSalez");
        message.To.Add(new MailAddress(toEmail));

        await client.SendMailAsync(message);

        return true;
    }
    
    public async Task<bool> VerifyEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User is not found!");
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