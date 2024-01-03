using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    
    public AccountRepository(ApplicationDbContext dbContext, ILogger<AccountRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task AddLimitsToAccountInDbAsync(Guid userId, UserType userType)
    {
        try
        {
            _logger.LogInformation($"Adding limits to user ID {userId} in DB");

            var user = await _dbContext.Users.FindAsync(userId);
            var limit = await _dbContext.AccountLimits
                .Where(x => x.UserType == userType)
                .FirstOrDefaultAsync();
        
            user.PremiumUploadLimit = limit.PremiumAnnouncementsLimit;
            user.RegularUploadLimit = limit.RegularAnnouncementsLimit;
            
            var response = _dbContext.Update(user);

            if (response.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error adding limits to account with ID {userId}");
            throw;
        }
    }

    public async Task<ApplicationUser> FindUserByLoginInDbAsync(string login)
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

            var removeResponse = _dbContext.Users.Remove(user);
            var addResponse = await _dbContext.AddAsync(defaultAccount);

            if (removeResponse.State == EntityState.Deleted && addResponse.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return defaultAccount;
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user with ID {user.Id} to DefaultAccount in DB");
            throw;
        }
    }

    public async Task<ApplicationUser> DeleteUserFromDbAsync(Guid userId)
    {
        try
        {
            _logger.LogInformation($"Deleting user with ID {userId} from DB");

            var user = await _dbContext.Users
                .Where(x => x.Id == userId)
                .Include(x => x.PhoneNumbers)
                .FirstOrDefaultAsync();
            var announcements = await _dbContext.Announcements
                .Where(x => x.Owner.Id == user.Id)
                .Include(x => x.ImageUrls)
                .ToListAsync();
            var images = announcements
                .SelectMany(a => a.ImageUrls)
                .ToList();
            
            _dbContext.ImageUrls.RemoveRange(images);
            _dbContext.Announcements.RemoveRange(announcements);
            _dbContext.AccountPhoneNumbers.RemoveRange(user.PhoneNumbers);
            
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error deleting user with ID {userId} from DB");
            throw;
        }
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToPremiumInDbAsync(ApplicationUser user)
    {
        try
        {
            _logger.LogInformation($"Updating user with ID {user.Id} to Premium in DB");

            if (user is PremiumAccount)
            {
                return new PremiumAccount();
            }

            var limit = await _dbContext.AccountLimits
                .Where(x => x.UserType == UserType.PremiumAccount)
                .FirstOrDefaultAsync();
        
            var premiumAccount = new PremiumAccount()
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

            if (removeResponse.State == EntityState.Deleted && addResponse.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return premiumAccount;
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user with ID {user.Id} to Premium in DB");
            throw;
        }
    }

    public async Task<ApplicationUser> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user)
    {
        try
        {
            _logger.LogInformation($"Updating user with ID {user.Id} to Business in DB");

            if (user is BusinessAccount)
            {
                return new BusinessAccount();
            }
        
            var limit = await _dbContext.AccountLimits
                .Where(x => x.UserType == UserType.BusinessAccount)
                .FirstOrDefaultAsync();
        
            var businessAccount = new BusinessAccount()
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
            var addResponse = await _dbContext.AddAsync(businessAccount);

            if (removeResponse.State == EntityState.Deleted && addResponse.State == EntityState.Added)
            {
                await _dbContext.SaveChangesAsync();
                return businessAccount;
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error updating user with ID {user.Id} to Business in DB");
            throw;
        }
    }
}