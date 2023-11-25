using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IPaymentService
{
    Task<bool> ProcessPayment(PaymentRequestDto request);
}