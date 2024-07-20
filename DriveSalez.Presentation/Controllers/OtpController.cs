using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class OtpController : Controller
{
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;
    
    public OtpController(IEmailService emailService, IOtpService otpService, IMemoryCache cache, ILogger<OtpController> logger)
    {
        _emailService = emailService;
        _otpService = otpService;
        _cache = cache;
        _logger = logger;
    }
    
    [HttpPost("send")]
    public async Task<ActionResult> SendOtpByEmail([FromBody] string email)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (_cache.TryGetValue(email, out string cachedOtp))
        {
            _cache.Remove(email);    
        }
        
        string otp = _otpService.GenerateOtp();
        string subject = "DriveSalez - One-Time Password (OTP)";
            
        string body = $"Thank you for choosing DriveSalez!\n\n" +
                      $"To verify your identity, please use the following One-Time Password (OTP):\n{otp}\n\n" +
                      $"This OTP is valid for 3 minutes and is used to ensure the security of your account.\n\n" +
                      $"Please do not share this OTP with anyone and avoid responding to any requests for it.\n\n" +
                      $"Best regards, DriveSalez Team";
                
            
        var response = await _emailService.SendEmailAsync(email, subject, body);

        if (!response) return BadRequest("Cannot send OTP");
        _cache.Set(email, otp, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        });

        return Ok("OTP was sent to your Email");
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> ValidateOtp([FromBody] ValidateOtpDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response =  await _otpService.ValidateOtpAsync(_cache, request);

        if (!response) return BadRequest("Cannot validate OTP");
        var result = await _emailService.VerifyEmailAsync(request.Email);
            
        if (result)
        {
            return Ok("Email was successfully verified");
        }

        return BadRequest("Cannot validate OTP");
    }
}