using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Core.Services;

public class OtpService : IOtpService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public OtpService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public string GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    public async Task<bool> ValidateOtp(IMemoryCache cache, ValidateOtpDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (cache.TryGetValue(request.Email, out string cachedOtp))
        {
            if (request.Otp == cachedOtp)
            {
                cache.Remove(request.Email);
                return true;
            }

            return false;
        }

        return false;
    }
}