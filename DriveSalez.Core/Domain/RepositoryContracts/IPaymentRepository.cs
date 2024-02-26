using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO;

namespace DriveSalez.Core.Domain.RepositoryContracts;

public interface IPaymentRepository
{
    Task<bool> RecordBalanceTopUpInDbAsync(ApplicationUser user, PaymentRequestDto request);

    Task<bool> AddPremiumAnnouncementLimitInDbAsync(ApplicationUser user, int announcementQuantity, int subscriptionId);

    Task<bool> AddRegularAnnouncementLimitInDbAsync(ApplicationUser user, int announcementQuantity, int subscriptionId);

    Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId);
}