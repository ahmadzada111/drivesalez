using DriveSalez.Core.DTO;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmailController : Controller
{
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    private readonly IMemoryCache _cache;
    
    public EmailController(IEmailService emailService, IOtpService otpService, IMemoryCache cache)
    {
        _emailService = emailService;
        _otpService = otpService;
        _cache = cache;
    }
    
    [HttpPost("otp/send")]
    public async Task<ActionResult> SendOtpByEmail([FromBody] string email)
    {
        if (_cache.TryGetValue(email, out string cachedOtp))
        {
            _cache.Remove(email);    
        }
        
        string otp = _otpService.GenerateOtp();
        var response = await _emailService.SendOtpByEmail(email, otp);

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

    [HttpPost("otp/validate")]
    public async Task<ActionResult> ValidateOtp([FromBody] ValidateOtpDto request)
    {
        var response =  await _otpService.ValidateOtp(_cache, request);

        if (response)
        {
            var result = await _emailService.VerifyEmail(request.Email);
            
            if (result)
            {
                return Ok("Email was successfully verified");
            }

            return BadRequest("User not found");
        }

        return BadRequest("Cannot validate OTP");
    }
    
    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        var result = await _emailService.ResetPassword(request);
        return result ? Ok("Password successfully changed") : BadRequest("Error");
    }
}