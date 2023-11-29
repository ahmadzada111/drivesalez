using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
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
        var user = await _dbContext.Users.
            Where(x => x.Id == userId).
            FirstOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        user.AccountBalance = request.Sum;
        return true;
    }

    public async Task<bool> AddPremiumAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId)
    {
        var user = await _dbContext.Users.
            Where(x => x.Id == userId).
            FirstOrDefaultAsync();
        var premiumAnnouncementSubscription = await _dbContext.Subscriptions.
            Include(x => x.Price)
            .Where(x => x.Id == subscriptionId)
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

        if (premiumAnnouncementSubscription == null)
        {
            throw new KeyNotFoundException();
        }

        if (user.AccountBalance - premiumAnnouncementSubscription.Price.Price > 0)
        {
            user.AccountBalance -= announcementQuantity * premiumAnnouncementSubscription.Price.Price;
            return true;
        }

        return false;
    }

    public async Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions.
            Include(x => x.Price).
            Where(x => x.Id == subscriptionId).
            FirstOrDefaultAsync();

        if (subscription == null)
        {
            throw new KeyNotFoundException();
        }

        return subscription;
    }
}