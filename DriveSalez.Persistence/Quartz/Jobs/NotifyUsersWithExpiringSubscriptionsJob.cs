using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class NotifyUsersWithExpiringSubscriptionsJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;
    
    public NotifyUsersWithExpiringSubscriptionsJob(ApplicationDbContext dbContext, ILogger<NotifyUsersWithExpiringSubscriptionsJob> logger, IEmailService emailService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(NotifyUsersWithExpiringSubscriptionsJob)} job started");
        //
        // var users = await _dbContext.Users
        //     .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now.AddDays(7))
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
        //     string subject = "Subscription Expiry Reminder";
        //     string body = $"Dear {user.FirstName} {user.LastName}," +
        //                   $"\n\nWe wanted to remind you that your subscription to our service will expire in 7 days." +
        //                   $" To ensure uninterrupted access and continue enjoying our premium features, we recommend renewing your subscription." +
        //                   $"\n\nThank you for being a valued member of our community." +
        //                   $"\n\nBest regards,\nDriveSalez Team";
        //
        //     var emailMetadata = new EmailMetadata(toAddress: user.Email, subject: subject, body: body);
        //     await _emailService.SendEmailAsync(emailMetadata);
        // }
        //
        // _logger.LogInformation($"{typeof(NotifyUsersWithExpiringSubscriptionsJob)} job finished");
    }
}