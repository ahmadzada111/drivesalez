using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
        
    public PaymentRepository(ApplicationDbContext dbContext, ILogger<PaymentRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> RecordBalanceTopUpInDbAsync(Guid userId, PaymentRequestDto request)
    {
        try
        {
            _logger.LogInformation($"Recording balance top up for user with ID {userId} in DB");
            
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Error recording balance top up for user with ID {userId} in DB");
            throw;
        }
    }

    public async Task<bool> AddAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId)
    {
        try
        {
            _logger.LogInformation($"Adding limit to user with ID {userId} in DB");
            
            var user = await _dbContext.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();
            
            var announcementSubscription = await _dbContext.AnnouncementPricing
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

            if (announcementSubscription.PricingName == "Premium Announcement")
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
            else if(announcementSubscription.PricingName == "Regular Announcement")
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Error adding limit to user with ID {userId} in DB");
            throw;
        }
    }

    public async Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId)
    {
        try
        {
            _logger.LogInformation($"Getting subscription with ID {subscriptionId} from DB");
            
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Error getting subscription with ID {subscriptionId} from DB");
            throw;
        }
    }
}