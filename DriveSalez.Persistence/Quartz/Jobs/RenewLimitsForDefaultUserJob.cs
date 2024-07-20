using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class RenewLimitsForDefaultUserJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public RenewLimitsForDefaultUserJob(ApplicationDbContext dbContext, ILogger<RenewLimitsForDefaultUserJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(RenewLimitsForDefaultUserJob)} job started");
        
        var users = await _dbContext.Users
            .Where(x => x.SubscriptionExpirationDate <= DateTimeOffset.Now)
            .Join(_dbContext.UserRoles,
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRole) => new
                    {
                        User = user, UserRole = userRole
                    })
            .Where(joined => _dbContext.Roles
                .Any(r => r.Id == joined.UserRole.RoleId && (r.Name == UserType.DefaultAccount.ToString())))
            .Select(joined => joined.User)
            .ToListAsync();
        
        var limit = await _dbContext.AccountLimits
            .Where(x => x.UserType == UserType.DefaultAccount)
            .FirstOrDefaultAsync();
        
        foreach (var user in users)
        {
            user.RegularUploadLimit = limit.RegularAnnouncementsLimit;
            user.PremiumUploadLimit = limit.PremiumAnnouncementsLimit;
            user.SubscriptionExpirationDate = DateTimeOffset.Now.AddMonths(1);
        }
        
        _dbContext.UpdateRange(users);
        await _dbContext.SaveChangesAsync();
            
        _logger.LogInformation($"{typeof(RenewLimitsForDefaultUserJob)} job finished");
    }
}