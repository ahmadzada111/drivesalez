using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Jobs;

public class RenewLimitsForDefaultUserJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public RenewLimitsForDefaultUserJob(ApplicationDbContext dbContext, ILogger<StartImageAnalyzerJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(RenewLimitsForDefaultUserJob)} job started");
        
        var users = await _dbContext.Users
            .OfType<DefaultAccount>()
            .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now)
            .ToListAsync();
        var limit = await _dbContext.AccountLimits
            .Where(x => x.UserType == UserType.DefaultAccount)
            .FirstOrDefaultAsync();
        
        foreach (var user in users)
        {
            user.RegularUploadLimit = limit.RegularAnnouncementsLimit;
            user.PremiumUploadLimit = limit.PremiumAnnouncementsLimit;
        }
        
        _dbContext.UpdateRange(users);
        await _dbContext.SaveChangesAsync();
            
        _logger.LogInformation($"{typeof(RenewLimitsForDefaultUserJob)} job finished");
    }
}