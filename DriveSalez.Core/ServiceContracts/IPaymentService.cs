using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IPaymentService
{
    bool ProcessPayment(PaymentRequestDto request);
}