using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.DTO;

namespace DriveSalez.Core.Domain.RepositoryContracts;

public interface IPaymentRepository
{
    Task<bool> RecordBalanceTopUpInDbAsync(Guid userId, PaymentRequestDto request);

    Task<bool> AddPremiumAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);

    Task<bool> AddRegularAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);

    Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId);
}