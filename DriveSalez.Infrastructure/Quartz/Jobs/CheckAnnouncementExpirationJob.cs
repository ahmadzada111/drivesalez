using DriveSalez.Core.Enums;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Jobs;

public class CheckAnnouncementExpirationJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public CheckAnnouncementExpirationJob(ApplicationDbContext dbContext, ILogger<CheckAnnouncementExpirationJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(CheckAnnouncementExpirationJob)} job started");
        
        var expiredAnnouncements = await _dbContext.Announcements
            .Where(a => a.AnnoucementState == AnnouncementState.Active && a.ExpirationDate <= DateTimeOffset.Now)
            .ToListAsync();
        
        foreach (var announcement in expiredAnnouncements)
        {
            announcement.AnnoucementState = AnnouncementState.Inactive;
        }
        
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation($"{typeof(CheckAnnouncementExpirationJob)} job finished");
    }
}