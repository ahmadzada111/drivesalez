using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

/// <summary>
/// Provides operations related to user account management.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/accounts")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger _logger;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userService">Service to handle account-related operations.</param>
    /// <param name="logger">Logger for the controller.</param>
    public AccountController(IUserService userService, ILogger<AccountController> logger, 
        IEmailService emailService)
    {
        _userService = userService;
        _logger = logger;
        _emailService = emailService;
    }

    /// <summary>
    /// Registers a user with DefaultAccount role
    /// </summary>
    /// <param name="request">Object with parameters to register user</param>
    /// <returns>
    /// Returns 200 if successfully registered
    /// Returns 500 if validation failed
    /// Returns 400 if registration failed
    /// </returns>
    [HttpPost("default")]
    public async Task<ActionResult> RegisterDefaultAccount([FromBody] RegisterAccountDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var response = await _userService.RegisterAccount(request, UserType.DefaultUser);

        if (!response.Succeeded)
        {
            return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
        }

        var user = await _userService.FindByEmail(request.Email);
        var token = await _userService.GenerateEmailConfirmationToken(user);
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail), 
            "Account", 
            new { userId = user.Id, token = token }, 
            Request.Scheme);

        await _emailService.SendEmailAsync(new EmailMetadata
        (
            toAddress: user.Email,
            subject: "Email Confirmation",
            body: $"Please confirm your account by clicking this link: {confirmationLink}"
        ));

        return Created();
    }

    /// <summary>
    /// Registers a user with the BusinessAccount role.
    /// </summary>
    /// <param name="request">Object with parameters to register the user.</param>
    /// <returns>
    /// Returns 200 if successfully registered.<br/>
    /// Returns 500 if validation failed.<br/>
    /// Returns 400 if registration failed.
    /// </returns>
    [HttpPost("business")]
    public async Task<ActionResult> RegisterBusinessAccount([FromBody] RegisterAccountDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var response = await _userService.RegisterAccount(request, UserType.BusinessUser);

        if (!response.Succeeded)
        {
            return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
        }

        return Created();
    }
    
    [HttpGet("email/confirmation")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto request)
    {
        var result = await _userService.ConfirmEmail(request.UserId, request.Token);
        
        if (result.Succeeded)
        {
            return Ok("Email confirmed successfully.");
        }

        return BadRequest(result.Errors);
    }
    
    /// <summary>
    /// Logs in a user and returns an AuthResponse with JWT and Refresh Token.
    /// </summary>
    /// <param name="request">Object with parameters to log in the user.</param>
    /// <returns>
    /// Returns 200 with AuthResponse if login is successful.<br/>
    /// Returns 500 if validation failed.<br/>
    /// Returns 401 if login fails.
    /// </returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> LoginAccount([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _userService.LoginAccount(request);

        return Ok(response);
    }
    
    /// <summary>
    /// Logs in staff and returns an AuthResponse with JWT and Refresh Token.
    /// </summary>
    /// <param name="request">Object with parameters to log in the staff.</param>
    /// <returns>
    /// Returns 200 with AuthResponse if login is successful.<br/>
    /// Returns 500 if validation failed.<br/>
    /// Returns 401 if login fails.
    /// </returns>
    [HttpPost("staff/login")]
    public async Task<ActionResult<AuthResponseDto>> LoginStaff([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _userService.LoginStaff(request);

        return Ok(response);
    }
    
    /// <summary>
    /// Logs out the user.
    /// </summary>
    /// <returns>
    /// Returns 204 (No Content) if logout is successful.
    /// </returns>
    [HttpPost("logout")]
    public async Task<ActionResult> LogOut()
    {
        
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        await _userService.LogOut();
        return NoContent();
    }
    
    /// <summary>
    /// Refreshes the JWT and Refresh Token.
    /// </summary>
    /// <param name="request">Object with parameters to refresh tokens.</param>
    /// <returns>
    /// Returns 200 with AuthResponse if refresh is successful.<br/>
    /// Returns 500 if there are problems during the refresh process.<br/>
    /// Returns 401 if the refresh fails.
    /// </returns>
    [HttpPost("token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshJwtDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _userService.RefreshToken(request);
        return Ok(response);
    }

    /// <summary>
    /// Changes the password for the authenticated user.
    /// </summary>
    /// <param name="request">Object with parameters to change the password.</param>
    /// <returns>
    /// Returns 200 if the password was successfully changed.<br/>
    /// Returns 500 if validation failed.<br/>
    /// Returns 400 if changing the password failed.
    /// </returns>
    [Authorize]
    [HttpPatch("password/change")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var result = await _userService.ChangePassword(request);
        return result ? Ok("Password was successfully changed") : BadRequest("Error");
    }
    
    [HttpPost("password/request-reset")]
    public async Task<ActionResult> RequestPasswordReset([FromBody] string email)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Password reset requested for: {email}");
        
        var user = await _userService.FindByEmail(email);
        var resetToken = await _userService.GeneratePasswordResetToken(user);
        var resetLink = Url.Action("ResetPassword", "Account", new
        {
            token = resetToken,
            email = user.Email
        }, protocol: HttpContext.Request.Scheme);

        await _emailService.SendEmailAsync(new EmailMetadata(user.Email, 
            "Password Reset",
            $"Reset your password by clicking this link: {resetLink}"));

        return Ok();
    }
    
    /// <summary>
    /// Resets the password using an OTP (One-Time Password).
    /// </summary>
    /// <param name="request">Object with parameters to reset the password.</param>
    /// <returns>
    /// Returns 200 if the password was successfully reset.<br/>
    /// Returns 500 if there are problems during the reset process.<br/>
    /// </returns>
    [HttpPatch("password/reset")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Password reset attempt for: {request.Email}");

        var user = await _userService.FindByEmail(request.Email);
        var result = await _userService.ResetPassword(user, request.Token, request.NewPassword);

        if (result.Succeeded)
        {
            return Ok("Password has been successfully reset.");
        }

        return BadRequest("Password reset failed. The token may be invalid or expired.");
    }
    
    [Authorize]
    [HttpPost("email/request-change")]
    public async Task<ActionResult> RequestEmailChange([FromBody] ChangeEmailDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var user = await _userService.FindByEmail(request.Email);
        var token = await _userService.GenerateChangeEmailToken(user, request.NewEmail);

        var confirmationLink = Url.Action(
            nameof(ChangeEmail),
            "User",
            new { userId = user.Id, newEmail = request.NewEmail, token = token },
            Request.Scheme); 

        await _emailService.SendEmailAsync(new EmailMetadata
            (
                toAddress: user.Email,
                subject: "Email Confirmation",
                body: $"Please confirm your account by clicking this link: {confirmationLink}"
            ));

        return Ok("Email change request has been initiated. Please confirm via the link sent to your new email.");
    }
    
    /// <summary>
    /// Changes the email address of the user using an OTP.
    /// </summary>
    /// <param name="request">Object with parameters to change the email address.</param>
    /// <returns>
    /// Returns 200 if the email address was successfully changed.<br/>
    /// Returns 500 if there are problems during the email change process.<br/>
    /// Returns 400 if OTP validation fails.
    /// </returns>
    [Authorize]
    [HttpPost("email")]
    public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailConfirmationDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var result = await _userService.ChangeEmail(request.UserId, request.NewEmail, request.Token);

        if (result.Succeeded)
        {
            return Ok("Email successfully changed.");
        }

        return BadRequest("Unable to confirm email change. The link may have expired or is invalid.");
    }
    
    /// <summary>
    /// Retrieves the user limits.
    /// </summary>
    /// <returns>
    /// Returns 200 with user limits if successfully retrieved.
    /// </returns>
    // [HttpGet("limits")]
    // public async Task<ActionResult> GetUserLimits()
    // {
    //     _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
    //     
    //     var response = await _userService.GetUserLimits();
    //     return Ok(response);
    // }
    
    /// <summary>
    /// Deletes the user account.
    /// </summary>
    /// <param name="password">The password of the user to delete the account.</param>
    /// <returns>
    /// Returns 204 if the user was successfully deleted.<br/>
    /// Returns 401 if the password is invalid.
    /// </returns>
    [Authorize]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser([FromBody] string password)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (string.IsNullOrEmpty(password))
        {
            return Unauthorized("Password is invalid");
        }
        
        await _userService.DeleteUser(password);
        return NoContent();
    }
}

