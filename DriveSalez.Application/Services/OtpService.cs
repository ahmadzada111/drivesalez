using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.Application.Services;

public class OtpService : IOtpService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public OtpService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public int GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999);
    }

    public async Task<bool> ValidateOtpAsync(IMemoryCache cache, ValidateOtpDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User with provided email wasn't found!");
        }
        
        if (cache.TryGetValue(request.Email, out int cachedOtp))
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