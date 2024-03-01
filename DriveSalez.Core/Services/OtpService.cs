using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
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

    public async Task<bool> ValidateOtpAsync(IMemoryCache cache, ValidateOtpDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User with provided email wasn't found!");
        }
        
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