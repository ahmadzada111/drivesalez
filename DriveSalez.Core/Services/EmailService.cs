using System.Net;
using System.Net.Mail;
using DriveSalez.Core.DTO;
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
    
    public async Task<bool> SendOtpByEmail(string toEmail, string otp)
    {
        var user = await _userManager.FindByEmailAsync(toEmail);
        
        if (user == null)
        {
            return false;
        }
        
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        
        client.Credentials = new NetworkCredential(
            _emailConfig["Email:CompanyEmail"],
            _emailConfig["Email:EmailKey"]);
    
        var message = new MailMessage()
        {
            Subject = "Email verification",
            Body = $"Your one time password: {otp}.\nPlease, don't answer to this mail.\nDo not share this password with anybody."
        };

        message.From = new MailAddress(_emailConfig["Email:CompanyEmail"], "DriveSalez");
        message.To.Add(new MailAddress(toEmail));

        await client.SendMailAsync(message);

        return true;
    }
    
    public async Task<bool> VerifyEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            return false;
        }
        
        user.EmailConfirmed = true;
        
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ResetPassword(string email, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            return false;
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
}