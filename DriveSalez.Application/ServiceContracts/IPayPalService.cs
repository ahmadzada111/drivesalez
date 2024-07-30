using PayPal.Api;

namespace DriveSalez.Application.ServiceContracts;

public interface IPayPalService
{
    Task<Order> CreateOrderAsync(string currency, decimal value, string returnUrl, string cancelUrl);
}