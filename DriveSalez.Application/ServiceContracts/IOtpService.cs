using DriveSalez.Application.DTO.AccountDTO;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Application.ServiceContracts;

public interface IOtpService
{
    int GenerateOtp();

    Task<bool> ValidateOtpAsync(IMemoryCache cache, ValidateOtpDto request);
}