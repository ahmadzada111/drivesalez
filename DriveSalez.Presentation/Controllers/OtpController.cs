using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class OtpController : Controller
{
    private readonly IEmailService _emailService;
    private readonly IAccountService _accountService;
    private readonly IOtpService _otpService;
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public OtpController(IEmailService emailService, IOtpService otpService,
        ILogger<OtpController> logger, IAccountService accountService, UserManager<ApplicationUser> userManager)
    {
        _emailService = emailService;
        _otpService = otpService;
        _logger = logger;
        _accountService = accountService;
        _userManager = userManager;
    }
    
    [HttpPost("send")]
    public async Task<ActionResult> SendOtpByEmail([FromBody] string email)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var user = await _userManager.FindByEmailAsync(email) ??
        throw new UserNotFoundException("User not found");

        int otp = _otpService.GenerateOtp();
        string subject = "DriveSalez - One-Time Password (OTP)";
        string body = $"Thank you for choosing DriveSalez!\n\n" +
                      $"To verify your identity, please use the following One-Time Password (OTP):\n{otp}\n\n" +
                      $"This OTP is valid for 3 minutes and is used to ensure the security of your account.\n\n" +
                      $"Please do not share this OTP with anyone and avoid responding to any requests for it.\n\n" +
                      $"Best regards, DriveSalez Team";

        var emailMetadata = new EmailMetadata(toAddress: user.Email, body: body, subject: subject);
        var response = await _emailService.SendEmailAsync(emailMetadata);

        if (!response) return BadRequest("Cannot send OTP");

        _otpService.WriteOtpToMemoryCache(email, otp);

        return Ok("OTP was sent to your Email");
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> ValidateOtp([FromBody] ValidateOtpDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response =  await _otpService.ValidateOtpAsync(request);

        if (!response) return BadRequest("Cannot validate OTP");
        var result = await _accountService.VerifyEmailAsync(request.Email);
            
        if (result)
        {
            return Ok("Email was successfully verified");
        }

        return BadRequest("Cannot validate OTP");
    }
}