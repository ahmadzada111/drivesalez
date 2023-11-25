namespace DriveSalez.Core.ServiceContracts;

public interface IPaymentRepository
{
    Task<bool> RecordPaymentInDbAsync(Guid userId);

}