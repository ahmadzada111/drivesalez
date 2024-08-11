using DriveSalez.Application.DTO;

namespace DriveSalez.Application.ServiceContracts;

public interface IOtpService
{
    int GenerateOtp();

    Task<bool> ValidateOtpAsync(ValidateOtpDto request);

    void RemoveOtpIfExists(string key);

    void WriteOtpToMemoryCache(string key, int value);
}