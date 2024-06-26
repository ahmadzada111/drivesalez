using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IPaymentService
{
    Task<bool> TopUpBalance(PaymentRequestDto request);

    // Task<bool> AddPremiumAnnouncementLimit(int announcementQuantity, int subscriptionId);
    //
    // Task<bool> AddRegularAnnouncementLimit(int announcementQuantity, int subscriptionId);

    Task<bool> AddAnnouncementLimit(int announcementQuantity, int subscriptionId);
    
    Task<bool> BuyPremiumAccount(int subscriptionId);

    Task<bool> BuyBusinessAccount(int subscriptionId);
}