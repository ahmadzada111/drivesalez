using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IPaymentService
{
    Task<bool> TopUpBalance(PaymentRequestDto request);

    Task<bool> AddAnnouncementLimit(int announcementQuantity, int subscriptionId);

    Task<bool> BuyPremiumAccount(int subscriptionId);

    Task<bool> BuyBusinessAccount(int subscriptionId);
}