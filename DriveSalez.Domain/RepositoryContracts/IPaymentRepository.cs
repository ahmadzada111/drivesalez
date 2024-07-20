using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IPaymentRepository
{
    // Task<bool> RecordBalanceTopUpInDbAsync(ApplicationUser user, PaymentRequestDto request);

    // Task<bool> AddPremiumAnnouncementLimitInDbAsync(ApplicationUser user, int announcementQuantity, int subscriptionId);
    //
    // Task<bool> AddRegularAnnouncementLimitInDbAsync(ApplicationUser user, int announcementQuantity, int subscriptionId);

    Task<bool> AddAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);
    
    Task<Subscription?> GetSubscriptionFromDbAsync(int subscriptionId);
}