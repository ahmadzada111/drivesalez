using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PaymentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> RecordBalanceTopUpInDbAsync(Guid userId, PaymentRequestDto request)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        user.AccountBalance += request.Sum;
        var response = _dbContext.Update(user);

        if (response.State == EntityState.Modified)
        {
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> AddAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();
        
        var announcementSubscription = await _dbContext.Subscriptions
            .Include(x => x.Price)
            .Where(x => x.Id == subscriptionId)
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

        if (announcementSubscription == null)
        {
            throw new KeyNotFoundException();
        }

        if (announcementSubscription.SubscriptionName == "Premium Announcement")
        {
            if (user.AccountBalance - announcementSubscription.Price.Price > 0)
            {
                user.AccountBalance -= announcementQuantity * announcementSubscription.Price.Price;
                user.PremiumUploadLimit += announcementQuantity;

                var response = _dbContext.Update(user);

                if (response.State == EntityState.Modified)
                {
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
        }
        else if(announcementSubscription.SubscriptionName == "Regular Announcement")
        {
            if (user.AccountBalance - announcementSubscription.Price.Price > 0)
            {
                user.AccountBalance -= announcementQuantity * announcementSubscription.Price.Price;
                user.RegularUploadLimit += announcementQuantity;

                var response = _dbContext.Update(user);

                if (response.State == EntityState.Modified)
                {
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
        }

        return false;
    }

    public async Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions
            .Include(x => x.Price)
            .Where(x => x.Id == subscriptionId)
            .FirstOrDefaultAsync();

        if (subscription == null)
        {
            throw new KeyNotFoundException();
        }

        return subscription;
    }
}