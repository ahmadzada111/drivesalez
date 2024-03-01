using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Jobs;

public class NotifyUserAboutSubscriptionCancellationJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAccountService _accountService;
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;
    
    public NotifyUserAboutSubscriptionCancellationJob(ApplicationDbContext dbContext, ILogger<StartImageAnalyzerJob> logger,
        IEmailService emailService, IAccountService accountService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
        _accountService = accountService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(NotifyUserAboutSubscriptionCancellationJob)} job started");
        
        var users = await _dbContext.Users
            .OfType<PaidUser>()
            .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now)
            .ToListAsync();

        foreach (var user in users)
        {
            await _accountService.ChangeUserTypeToDefaultAccountAsync(user);
            
            string subject = "Your Subscription Has Been Canceled";
            string body = $"Dear {user.FirstName} {user.LastName},\n\nWe hope this message finds you well. " +
                          $"We regret to inform you that your subscription with DriveSalez has been canceled due to non-payment." +
                          $"\n\nReason for Cancellation:\nUnfortunately, we did not receive payment for your subscription, and as a result, your account has been set to the default status." +
                          $"\n\nAction Required:\nIf you believe this is an error or if you would like to reinstate your subscription, please log in to your account and update your payment information." +
                          $"\n\nAccount Status:\n- Username: {user.UserName}\n- Account Status: Default\n- Subscription Expiration Date: {user.SubscriptionExpirationDate}" +
                          $"\n\nContact Us:\nIf you have any questions or concerns, please feel free to contact our support team." +
                          $"\n\nWe appreciate your understanding and prompt attention to this matter.\n\nBest regards,\n\nDriveSalez Team";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
        
        _logger.LogInformation($"{typeof(NotifyUserAboutSubscriptionCancellationJob)} job finished");
    }
}