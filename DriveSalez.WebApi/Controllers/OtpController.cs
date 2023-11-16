using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class OtpController : Controller
{
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    private readonly IMemoryCache _cache;
    
    public OtpController(IEmailService emailService, IOtpService otpService, IMemoryCache cache)
    {
        _emailService = emailService;
        _otpService = otpService;
        _cache = cache;
    }
    
    [HttpPost("send")]
    public async Task<ActionResult> SendOtpByEmail([FromBody] string email)
    {
        if (_cache.TryGetValue(email, out string cachedOtp))
        {
            _cache.Remove(email);    
        }

        try
        {
            string otp = _otpService.GenerateOtp();
            string subject = "DriveSalez - One-Time Password (OTP)";
            string body = $"Thank you for choosing DriveSalez! " +
                          $"To verify your personality, please use the following One-Time Password (OTP):" +
                          $"\n\nYour OTP: {otp}\n\n" +
                          $"This OTP is valid for a 3 minutes and is used to ensure the security of your account. " +
                          $"Please do not share this OTP with anyone, and avoid responding to any requests for it." +
                          $"\n\nBest regards,\nDriveSalez Team";
            
            var response = await _emailService.SendEmailAsync(email, subject, body);

            if (response)
            {
                _cache.Set(email, otp, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                });

                return Ok("OTP was sent to your Email");
            }

            return BadRequest("Cannot send OTP");
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> ValidateOtp([FromBody] ValidateOtpDto request)
    {
        try
        {
            var response =  await _otpService.ValidateOtpAsync(_cache, request);

            if (response)
            {
                var result = await _emailService.VerifyEmailAsync(request.Email);
            
                if (result)
                {
                    return Ok("Email was successfully verified");
                }
            }

            return BadRequest("Cannot validate OTP");
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}