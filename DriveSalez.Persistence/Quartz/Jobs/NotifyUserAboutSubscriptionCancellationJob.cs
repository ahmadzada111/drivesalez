using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class NotifyUserAboutSubscriptionCancellationJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserService _userService;
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;
    
    public NotifyUserAboutSubscriptionCancellationJob(ApplicationDbContext dbContext, ILogger<NotifyUserAboutSubscriptionCancellationJob> logger,
        IEmailService emailService, IUserService userService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
        _userService = userService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // _logger.LogInformation($"{typeof(NotifyUserAboutSubscriptionCancellationJob)} job started");
        //
        // var users = await _dbContext.Users
        //     .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now)
        //     .ToListAsync();
        //
        // foreach (var user in users)
        // {
        //     if (string.IsNullOrWhiteSpace(user.Email))
        //     {
        //         _logger.LogWarning($"User {user.Id} does not have a valid email address.");
        //         continue;
        //     }
        //     
        //     await _userService.ChangeUserTypeToDefaultAccount(user);
        //     
        //     string subject = "Your Subscription Has Been Canceled";
        //     string body = $"Dear {user.FirstName} {user.LastName},\n\nWe hope this message finds you well. " +
        //                   $"We regret to inform you that your subscription with DriveSalez has been canceled due to non-payment." +
        //                   $"\n\nReason for Cancellation:\nUnfortunately, we did not receive payment for your subscription, and as a result, your account has been set to the default status." +
        //                   $"\n\nAction Required:\nIf you believe this is an error or if you would like to reinstate your subscription, please log in to your account and update your payment information." +
        //                   $"\n\nAccount Status:\n- Username: {user.UserName}\n- Account Status: Default\n- Subscription Expiration Date: {user.SubscriptionExpirationDate}" +
        //                   $"\n\nContact Us:\nIf you have any questions or concerns, please feel free to contact our support team." +
        //                   $"\n\nWe appreciate your understanding and prompt attention to this matter.\n\nBest regards,\n\nDriveSalez Team";
        //
        //     var emailMetadata = new EmailMetadata(toAddress: user.Email, subject: subject, body: body);
        //     await _emailService.SendEmailAsync(emailMetadata);
        // }
        //
        // _logger.LogInformation($"{typeof(NotifyUserAboutSubscriptionCancellationJob)} job finished");
    }
}