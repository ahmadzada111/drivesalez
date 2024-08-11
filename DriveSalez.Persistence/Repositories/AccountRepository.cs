using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BusinessAccount = DriveSalez.Domain.IdentityEntities.BusinessAccount;

namespace DriveSalez.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public AccountRepository(ApplicationDbContext dbContext, ILogger<AccountRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task AddLimitsToAccountInDbAsync(ApplicationUser user, UserType userType)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation($"Adding limits to user ID {user.Id} in DB");
            
            var limit = await _dbContext.AccountLimits
                .Where(x => x.UserType == userType)
                .FirstOrDefaultAsync() ?? 
                throw new InvalidOperationException($"Limit with {userType} - type wasn't found");
        
            user.PremiumUploadLimit = limit.PremiumAnnouncementsLimit;
            user.RegularUploadLimit = limit.RegularAnnouncementsLimit;
            
            _dbContext.Update(user);
            
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"Error adding limits to account with ID {user.Id}");
            throw;
        }
    }

    public async Task<ApplicationUser?> FindUserByLoginInDbAsync(string login)
    {
        try
        {
            _logger.LogInformation($"Getting user by login {login} from DB");
            
            var user = await _dbContext.Users
                .Where(x => x.UserName == login)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync();
        
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting user by login {login} from DB");
            throw;
        }
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation($"Updating user with ID {user.Id} to DefaultAccount in DB");

            var defaultAccount = new DefaultAccount
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumbers = user.PhoneNumbers,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = true,
                SecurityStamp = user.SecurityStamp,
                CreationDate = user.CreationDate,
                LastUpdateDate = user.LastUpdateDate,
                SubscriptionExpirationDate = DateTimeOffset.Now.AddMonths(1)
            };

            _dbContext.Users.Remove(user);
            await _dbContext.AddAsync(defaultAccount);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return defaultAccount;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"Error updating user with ID {user.Id} to DefaultAccount in DB");
            throw;
        }
    }

    public async Task<ApplicationUser> DeleteUserFromDbAsync(ApplicationUser user)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        
        try
        {
            _logger.LogInformation($"Deleting user with ID {user} from DB");
            
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();    
           
            return user;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"Error deleting user with ID {user} from DB");
            throw;
        }
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation($"Updating user with ID {user.Id} to Premium in DB");

            if (user is BusinessAccount)
            {
                return new BusinessAccount();
            }

            var limit = await _dbContext.AccountLimits
                .Where(x => x.UserType == UserType.BusinessAccount)
                .FirstOrDefaultAsync() ??
                throw new InvalidOperationException($"Limit with {UserType.BusinessAccount} - type wasn't found");;
        
            var premiumAccount = new BusinessAccount()
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumbers = user.PhoneNumbers,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = true,
                SecurityStamp = user.SecurityStamp,
                CreationDate = user.CreationDate,
                LastUpdateDate = user.LastUpdateDate,
                PremiumUploadLimit = limit.PremiumAnnouncementsLimit + user.PremiumUploadLimit,
                AccountBalance = user.AccountBalance,
                SubscriptionExpirationDate = DateTimeOffset.Now.AddMonths(1)
            };

            var removeResponse = _dbContext.Users.Remove(user);
            var addResponse = await _dbContext.AddAsync(premiumAccount);

            return premiumAccount;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"Error updating user with ID {user.Id} to Premium in DB");
            throw;
        }
    }
}