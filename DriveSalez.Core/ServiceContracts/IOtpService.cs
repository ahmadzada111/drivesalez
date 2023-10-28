using DriveSalez.Core.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Core.ServiceContracts;

public interface IOtpService
{
    string GenerateOtp();

    Task<bool> ValidateOtpAsync(IMemoryCache cache, ValidateOtpDto request);
}