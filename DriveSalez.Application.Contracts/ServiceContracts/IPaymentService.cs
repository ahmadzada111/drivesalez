namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IPaymentService
{
    // Task<bool> TopUpBalance(PaymentRequestDto request);

    // Task<bool> AddPremiumAnnouncementLimit(int announcementQuantity, int subscriptionId);
    //
    // Task<bool> AddRegularAnnouncementLimit(int announcementQuantity, int subscriptionId);

    Task<bool> AddAnnouncementLimitAsync(int announcementQuantity, int subscriptionId);

    // Task<bool> BuyBusinessAccountAsync(int subscriptionId);
}