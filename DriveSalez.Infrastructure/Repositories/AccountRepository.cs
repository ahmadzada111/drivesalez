using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddLimitsToAccountInDbAsync(Guid userId, UserType userType)
    {
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

    public async Task<ApplicationUser> FindUserByLoginInDbAsync(string login)
    {
        var user = await _dbContext.Users
            .Where(x => x.UserName == login)
            .Include(x => x.PhoneNumbers)
            .FirstOrDefaultAsync();
        
        return user;
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user)
    {
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
            LastUpdateDate = user.LastUpdateDate
            
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
    
    public async Task<ApplicationUser> ChangeUserTypeToPremiumInDbAsync(ApplicationUser user)
    {
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
            AccountBalance = user.AccountBalance
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

    public async Task<ApplicationUser> ChangeUserTypeToBusinessInDbAsync(ApplicationUser user)
    {
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
            AccountBalance = user.AccountBalance
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
}