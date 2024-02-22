using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Jobs;

public class NotifyUsersWithExpiringSubscriptionsJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IComputerVisionService _computerVisionService;
    private readonly IEmailService _emailService;
    
    public NotifyUsersWithExpiringSubscriptionsJob(ApplicationDbContext dbContext, ILogger<StartImageAnalyzerJob> logger,
        IComputerVisionService computerVisionService, IEmailService emailService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _computerVisionService = computerVisionService;
        _emailService = emailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(NotifyUsersWithExpiringSubscriptionsJob)} job started");
        
        var users = await _dbContext.Users
            .OfType<PaidUser>()
            .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now.AddDays(7))
            .ToListAsync();

        foreach (var user in users)
        {
            string subject = "Subscription Expiry Reminder";
            string body = $"Dear {user.FirstName} {user.LastName}," +
                          $"\n\nWe wanted to remind you that your subscription to our service will expire in 7 days." +
                          $" To ensure uninterrupted access and continue enjoying our premium features, we recommend renewing your subscription." +
                          $"\n\nThank you for being a valued member of our community." +
                          $"\n\nBest regards,\nDriveSalez Team";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
        
        _logger.LogInformation($"{typeof(NotifyUsersWithExpiringSubscriptionsJob)} job finished");
    }
}