using PayPal.Api;

namespace DriveSalez.Persistence.Contracts.ServiceContracts;

public interface IPayPalService
{
    Task<Order> CreateOrderAsync(string currency, decimal value, string returnUrl, string cancelUrl);
}