using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
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

        user.PremiumUploadLimit = limit;

        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ApplicationUser> FindUserByLoginInDbAsync(string login)
    {
        var user = await _dbContext.Users.
            Where(x => x.UserName == login).
            FirstOrDefaultAsync();

        return user;
    }
}