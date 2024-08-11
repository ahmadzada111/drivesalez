using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Application.Services;

public class OtpService : IOtpService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMemoryCache _memoryCache;

    public OtpService(UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
    {
        _userManager = userManager;
        _memoryCache = memoryCache;
    }

    public int GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999);
    }

    public void RemoveOtpIfExists(string key)
    {
        if (_memoryCache.TryGetValue(key, out int? cachedOtp))
        {
            _memoryCache.Remove(key);    
        }
    }

    public void WriteOtpToMemoryCache(string key, int value)
    {
        _memoryCache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
            Size = 1
        });
    }

    public async Task<bool> ValidateOtpAsync(ValidateOtpDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ??
        throw new UserNotFoundException("User with provided email wasn't found!");

        if (_memoryCache.TryGetValue(request.Email, out int cachedOtp))
        {
            if (request.Otp == cachedOtp)
            {
                _memoryCache.Remove(request.Email);
                return true;
            }

            return false;
        }

        return false;
    }
}