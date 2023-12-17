using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;

namespace DriveSalez.Core.RepositoryContracts;

public interface IPaymentRepository
{
    Task<bool> RecordBalanceTopUpInDbAsync(Guid userId, PaymentRequestDto request);

    Task<bool> AddAnnouncementLimitInDbAsync(Guid userId, int announcementQuantity, int subscriptionId);

    Task<Subscription> GetSubscriptionFromDbAsync(int subscriptionId);
}