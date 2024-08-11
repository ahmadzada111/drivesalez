using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IOtpService _otpService;
    private readonly ILogger _logger;
    
    public AccountController(IAccountService accountService, IOtpService otpService, IMemoryCache cache, 
        ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _otpService = otpService;
        _logger = logger;
    }

    [HttpPost("register-default-account")]
    public async Task<ActionResult> RegisterDefaultAccount([FromBody] RegisterDefaultAccountDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var response = await _accountService.RegisterDefaultAccountAsync(request);

        if (!response.Succeeded)
        {
            return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
        }

        return Ok();
    }

    [HttpPost("register-business-account")]
    public async Task<ActionResult> RegisterBusinessAccount([FromBody] RegisterBusinessAccountDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var response = await _accountService.RegisterBusinessAccountAsync(request);

        if (!response.Succeeded)
        {
            return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
        }

        return Ok();
    }
    
    [HttpPost("login-default-account")]
    public async Task<ActionResult<DefaultAccountAuthResponseDto>> LoginDefaultAccount([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _accountService.LoginDefaultAccountAsync(request);

        if (response == null)
        {
            return Unauthorized("Email or password is invalid");
        }

        return Ok(response);
    }
    
    [HttpPost("login-business-account")]
    public async Task<ActionResult<DefaultAccountAuthResponseDto>> LoginBusinessAccount([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _accountService.LoginBusinessAccountAsync(request);

        if (response == null)
        {
            return Unauthorized("Email or password is invalid");
        }

        return Ok(response);
    }

    [HttpPost("login-staff")]
    public async Task<ActionResult<DefaultAccountAuthResponseDto>> LoginStaff([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _accountService.LoginStaffAsync(request);

        if (response == null)
        {
            return Unauthorized("Email or password is invalid");
        }

        return Ok(response);
    }
    
    [HttpGet("logout")]
    public async Task<ActionResult> LogOut()
    {
        
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        await _accountService.LogOutAsync();
        return NoContent();
    }
    
    [HttpPost("refresh-business-account")]
    public async Task<ActionResult<BusinessAccountAuthResponseDto>> RefreshBusinessAccount([FromBody] RefreshJwtDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        
        var response = await _accountService.RefreshBusinessAccountAsync(request);
        return Ok(response);
    }
    
    [HttpPost("refresh-default-account")]
    public async Task<ActionResult<DefaultAccountAuthResponseDto>> RefreshDefaultAccount([FromBody] RefreshJwtDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        
        var response = await _accountService.RefreshDefaultAccountAsync(request);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        
        var result = await _accountService.ChangePasswordAsync(request);
        return result ? Ok("Password was successfully changed") : BadRequest("Error");
    }
    
    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response =  await _otpService.ValidateOtpAsync(request.ValidateRequest);

        if (!response) return BadRequest("Cannot validate OTP");
        var result = await _accountService.ResetPasswordAsync(request.ValidateRequest.Email, request.NewPassword);
        
        if (result)
        {
            return Ok("Password was successfully changed");
        }

        return BadRequest("Cannot validate OTP");
    }
    
    [Authorize]
    [HttpPost("change-email")]
    public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response =  await _otpService.ValidateOtpAsync(request.ValidateRequest);

        if (!response) return BadRequest("Cannot validate OTP");
        var result = await _accountService.ChangeEmailAsync(request.ValidateRequest.Email, request.NewMail);
        
        if (result)
        {
            return Ok("Password was successfully changed");
        }

        return BadRequest("Cannot validate OTP");
    }
    
    [Authorize]
    [HttpDelete("delete-user")]
    public async Task<ActionResult> DeleteUser([FromBody] string password)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (string.IsNullOrEmpty(password))
        {
            return Unauthorized("Password is invalid");
        }
        
        var response = await _accountService.DeleteUserAsync(password);
        return Ok(response);
    }
}

