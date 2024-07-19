using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class DeleteInactiveAccountsJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public DeleteInactiveAccountsJob(ApplicationDbContext dbContext, ILogger<DeleteInactiveAccountsJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(DeleteInactiveAccountsJob)} job started");
  
        var thresholdDate = DateTimeOffset.Now.AddDays(-30);
        var inactiveAccounts = await _dbContext.Users
            .Where(a => !a.EmailConfirmed && a.CreationDate <= thresholdDate)
            .Join(_dbContext.UserRoles,
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRole) => new
                {
                    User = user, UserRole = userRole
                })
            .Where(joined => !_dbContext.Roles
                .Any(r => r.Id == joined.UserRole.RoleId && (r.Name == UserType.Moderator.ToString() || r.Name == UserType.Admin.ToString() )))
            .Select(joined => joined.User)
            .ToListAsync();
        
        
        _dbContext.Users.RemoveRange(inactiveAccounts);
        
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation($"{typeof(DeleteInactiveAccountsJob)} job finished");
    }
}