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
    
    public async Task AddPremiumLimitToPaidAccountInDbAsync(Guid userId, UserType userType)
    {
        var user = (PaidUser) await _dbContext.Users.FindAsync(userId);
        var limit = await _dbContext.PaidAccountLimits.
            Where(x => x.UserType == userType).
            FirstOrDefaultAsync();

        user.PremiumUploadLimit = limit.PremiumAnnouncementsLimit;

        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ApplicationUser>? FindUserByLoginInDbAsync(string login)
    {
        var user = await _dbContext.Users.
            Where(x => x.UserName == login).
            FirstOrDefaultAsync();
        
        return user;
    }
    
    public async Task<ApplicationUser>? ChangeUserTypeToDefaultAccountInDbAsync(ApplicationUser user)
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

        _dbContext.Users.Remove(user);

        await _dbContext.AddAsync(defaultAccount);

        await _dbContext.SaveChangesAsync();
        
        return defaultAccount;
    }
    
    public async Task<ApplicationUser>? ChangeUserTypeToPremiumInDbAsync(ApplicationUser user)
    {
        if (user is PremiumAccount)
        {
            return new PremiumAccount();
        }

        var limit = await _dbContext.PaidAccountLimits.
            Where(x => x.UserType == UserType.PremiumAccount)
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

        _dbContext.Users.Remove(user);

        await _dbContext.AddAsync(premiumAccount);

        await _dbContext.SaveChangesAsync();
        
        return premiumAccount;
    }

    public async Task<ApplicationUser>? ChangeUserTypeToBusinessInDbAsync(ApplicationUser user)
    {
        if (user is BusinessAccount)
        {
            return new BusinessAccount();
        }
        
        var limit = await _dbContext.PaidAccountLimits.
            Where(x => x.UserType == UserType.BusinessAccount)
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

        _dbContext.Users.Remove(user);

        await _dbContext.AddAsync(businessAccount);

        await _dbContext.SaveChangesAsync();
        
        return businessAccount;
    }
}