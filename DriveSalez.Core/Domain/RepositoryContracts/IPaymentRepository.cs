using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;

namespace DriveSalez.Core.Domain.RepositoryContracts;

public interface IPaymentRepository
{
    Task<bool> RecordBalanceTopUpInDbAsync(Guid userId, PaymentRequestDto request);

    Task<bool> AddPremiumAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);

    Task<bool> AddRegularAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);

    Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId);
}