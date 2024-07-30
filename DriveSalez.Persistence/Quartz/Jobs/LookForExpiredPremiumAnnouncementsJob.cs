using DriveSalez.Application.ServiceContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class LookForExpiredPremiumAnnouncementsJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;

    public LookForExpiredPremiumAnnouncementsJob(ApplicationDbContext dbContext, ILogger<LookForExpiredPremiumAnnouncementsJob> logger,
        IEmailService emailService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(LookForExpiredPremiumAnnouncementsJob)} job started");
        
        var announcements = await _dbContext.Announcements
            .Where(a => a.IsPremium && a.PremiumExpirationDate <= DateTimeOffset.Now)
            .Include(a => a.Owner)
            .Include(a => a.Vehicle)
            .Include(a => a.Vehicle.Make)
            .Include(a => a.Vehicle.Model)
            .ToListAsync();

        foreach (var announcement in announcements)
        {
            if (string.IsNullOrWhiteSpace(announcement.Owner.Email))
            {
                _logger.LogWarning($"User {announcement.Owner.Id} does not have a valid email address.");
                continue;
            }
            
            announcement.IsPremium = false;

            string subject = "Your Premium Announcement Subscription Has Expired";
            string body = $"Dear {announcement.Owner.FirstName} {announcement.Owner.LastName}," +
                          "\n\nWe hope you've been enjoying the premium features of [Your Service Name]. " +
                          $"We wanted to inform you that your premium subscription on {announcement.Vehicle.Make} {announcement.Vehicle.Model} has expired as of {announcement.PremiumExpirationDate}." +
                          "\n\nPremium Benefits:\n- Your announcements appear at the top of user listings.\n- " +
                          "Has higher chances to be sold\n\n" +
                          "To continue enjoying these premium benefits, please log in to your account and renew your announcement subscription." +
                          "\n\nAct now to avoid any interruption in your premium access. " +
                          $"If you choose not to renew, your announcement will automatically revert to our regular free plan starting from {announcement.PremiumExpirationDate}." +
                          "\n\nThank you for choosing DriveSalez." +
                          "\n\nBest regards," +
                          "\n\nDriveSalez Team";

            await _emailService.SendEmailAsync(announcement.Owner.Email, subject, body);
        }
        
        _dbContext.UpdateRange(announcements);
        
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation($"{typeof(LookForExpiredPremiumAnnouncementsJob)} job finished");
    }
}