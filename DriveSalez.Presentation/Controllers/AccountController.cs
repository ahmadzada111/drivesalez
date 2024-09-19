using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.Contracts.ServiceContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using DriveSalez.SharedKernel.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    private readonly IValidator<RegisterAccountDto> _registerAccountDtoValidator;
    private readonly IValidator<LoginDto> _loginDtoValidator;
    private readonly IValidator<ConfirmEmailDto> _confirmEmailDtoValidator;
    private readonly IValidator<RefreshJwtDto> _refreshJwtDtoValidator;
    private readonly IValidator<ChangePasswordDto> _changePasswordDtoValidator;
    private readonly IValidator<ResetPasswordDto> _resetPasswordDtoValidator;
    private readonly IValidator<ChangeEmailDto> _changeEmailDtoValidator;
    private readonly IValidator<ChangeEmailConfirmationDto> _changeEmailConfirmationDtoValidator;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userService">The service responsible for user-related operations.</param>
    /// <param name="logger">The logger used for logging information and errors.</param>
    /// <param name="emailService">The service responsible for sending emails.</param>
    /// <param name="registerAccountDtoValidator">The validator for <see cref="RegisterAccountDto"/>.</param>
    /// <param name="loginDtoValidator">The validator for <see cref="LoginDto"/>.</param>
    /// <param name="confirmEmailDtoValidator">The validator for <see cref="ConfirmEmailDto"/>.</param>
    /// <param name="refreshJwtDtoValidator">The validator for <see cref="RefreshJwtDto"/>.</param>
    /// <param name="changePasswordDtoValidator">The validator for <see cref="ChangePasswordDto"/>.</param>
    /// <param name="resetPasswordDtoValidator">The validator for <see cref="ResetPasswordDto"/>.</param>
    /// <param name="changeEmailDtoValidator">The validator for <see cref="ChangeEmailDto"/>.</param>
    /// <param name="changeEmailConfirmationDtoValidator">The validator for <see cref="ChangeEmailConfirmationDto"/>.</param>
    public AccountController(IUserService userService, ILogger<AccountController> logger, 
        IEmailService emailService, IValidator<RegisterAccountDto> registerAccountDtoValidator,
        IValidator<LoginDto> loginDtoValidator, IValidator<ConfirmEmailDto> confirmEmailDtoValidator, 
        IValidator<RefreshJwtDto> refreshJwtDtoValidator, IValidator<ChangePasswordDto> changePasswordDtoValidator, 
        IValidator<ResetPasswordDto> resetPasswordDtoValidator, IValidator<ChangeEmailDto> changeEmailDtoValidator, IValidator<ChangeEmailConfirmationDto> changeEmailConfirmationDtoValidator)
    {
        _userService = userService;
        _logger = logger;
        _emailService = emailService;
        _registerAccountDtoValidator = registerAccountDtoValidator;
        _loginDtoValidator = loginDtoValidator;
        _confirmEmailDtoValidator = confirmEmailDtoValidator;
        _refreshJwtDtoValidator = refreshJwtDtoValidator;
        _changePasswordDtoValidator = changePasswordDtoValidator;
        _resetPasswordDtoValidator = resetPasswordDtoValidator;
        _changeEmailDtoValidator = changeEmailDtoValidator;
        _changeEmailConfirmationDtoValidator = changeEmailConfirmationDtoValidator;
    }

    /// <summary>
    /// Registers a new user account and optionally uploads profile and background photos.
    /// </summary>
    /// <param name="request">The details required to register the account, such as email and password.</param>
    /// <param name="profilePhoto">An optional profile photo file to be uploaded for the new user.</param>
    /// <param name="backgroundPhoto">An optional background photo file to be uploaded for the new user.</param>
    /// <returns>
    /// Returns a 201 Created response with a location header if the registration is successful.<br/>
    /// Returns a 400 Bad Request response if the registration data is invalid or if the user registration fails.<br/>
    /// Returns a 500 Internal Server Error response if there is an unexpected error during processing.
    /// </returns>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Validates the user registration data using a validator.
    /// 2. If the data is valid, processes any uploaded profile or background photos.
    /// 3. Registers the user account with the provided details and uploaded photos (if any).
    /// 4. Generates an email confirmation token and sends a confirmation email to the user.
    /// 5. Returns a response indicating the result of the registration process.
    ///
    /// Note: Email confirmation is required to activate the user account. The confirmation link will be sent to the user’s email address.
    /// </remarks>
    [HttpPost("signup")]
    public async Task<ActionResult> RegisterAccount([FromBody] RegisterAccountDto request, IFormFile? profilePhoto, IFormFile? backgroundPhoto)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _registerAccountDtoValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        FileUploadData? profilePhotoData = profilePhoto is not null
            ? new FileUploadData
            {
                FileType = profilePhoto.ContentType,
                Stream = profilePhoto.OpenReadStream()
            }
            : null;

        FileUploadData? backgroundPhotoData = backgroundPhoto is not null
            ? new FileUploadData
            {
                FileType = backgroundPhoto.ContentType,
                Stream = backgroundPhoto.OpenReadStream()
            }
            : null;
        
        var response = await _userService.RegisterAccount(request, profilePhotoData, backgroundPhotoData, Enum.Parse<UserType>(request.UserType, true));

        if (!response.Succeeded)
        {
            return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
        }

        var user = await _userService.FindByEmail(request.Email);
        var token = await _userService.GenerateEmailConfirmationToken(user);
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail), 
            "Account", 
            new { userId = user.Id, token }, 
            Request.Scheme);

        await _emailService.SendEmailAsync(new EmailMetadata
        (
            toAddress: user.Email!,
            subject: "Email Confirmation",
            body: $"Please confirm your account by clicking this link: {confirmationLink}"
        ));
        
        return CreatedAtAction(nameof(RegisterAccount), new { userId = user.Id }, "User registered successfully.");
    }
    
    /// <summary>
    /// Confirms the email address of a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="token">The email confirmation token.</param>
    /// <returns>
    /// Returns 200 (OK) if the email confirmation is successful.<br/>
    /// Returns 400 (Bad Request) if the confirmation token is invalid or has expired.<br/>
    /// Returns 500 (Internal Server Error) if there are validation errors or other issues.
    /// </returns>
    /// <remarks>
    /// This endpoint is used to confirm a user's email address by validating the provided user ID and token.
    /// The <paramref name="userId"/> is passed in the route, and the <paramref name="token"/> is passed as a query parameter.
    /// </remarks>
    [HttpPost("{userId}/email")]
    public async Task<ActionResult> ConfirmEmail([FromRoute] Guid userId, [FromQuery] string token)
    {
        var request = new ConfirmEmailDto { Token = token, UserId = userId };
        var validationResult = await _confirmEmailDtoValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var user = await _userService.FindById(userId);
        var result = await _userService.ConfirmEmail(user, request.Token);
        
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
    [HttpPost("signin")]
    public async Task<ActionResult<AuthResponseDto>> LoginAccount([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _loginDtoValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _userService.LoginAccount(request);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the profile information of a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose profile is being retrieved.</param>
    /// <returns>An action result containing the user's profile information.</returns>
    [Authorize]
    [HttpGet("{userId}/profile")]
    public async Task<ActionResult> Profile([FromRoute] Guid userId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var identityUser = await _userService.FindAuthorizedUser();
        var response = await _userService.FindUserProfile(userId, identityUser);
        return Ok(response);
    }
    
    /// <summary>
    /// Allows a user with the "Business" role to update their profile photo.
    /// </summary>
    /// <param name="userId">The ID of the user whose profile photo is to be updated.</param>
    /// <param name="photo">The new profile photo file to be uploaded.</param>
    /// <returns>
    /// Returns a 200 OK response if the profile photo was successfully updated.<br/>
    /// Returns a 400 Bad Request response if the file is null or if there is an issue with the upload process.<br/>
    /// Returns a 404 Not Found response if the user with the specified ID does not exist.<br/>
    /// Returns a 500 Internal Server Error response if there is an unexpected error during processing.
    /// </returns>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Validates the incoming file to ensure it is not null.
    /// 2. Retrieves the user by their ID from the database.
    /// 3. Uploads the new profile photo and associates it with the user.
    /// 4. Returns an appropriate response based on the result of the operation.
    ///
    /// Note: This endpoint requires the user to have the "Business" role to access it.
    /// </remarks>
    [Authorize(Roles = "Business")]
    [HttpPatch("{userId}/profile/photo")]
    public async Task<ActionResult> ChangeProfilePhoto([FromRoute] Guid userId, IFormFile photo)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var fileUploadData = new FileUploadData
        {
            FileType = photo.ContentType,
            Stream = photo.OpenReadStream()
        };

        var user = await _userService.FindById(userId);
        await _userService.ChangeProfilePhoto(fileUploadData, user);
        return Ok();
    }
    
    /// <summary>
    /// Allows a user with the "Business" role to update their background photo.
    /// </summary>
    /// <param name="userId">The ID of the user whose background photo is to be updated.</param>
    /// <param name="photo">The new background photo file to be uploaded.</param>
    /// <returns>
    /// Returns a 200 OK response if the background photo was successfully updated.<br/>
    /// Returns a 400 Bad Request response if the file is null or if there is an issue with the upload process.<br/>
    /// Returns a 404 Not Found response if the user with the specified ID does not exist.<br/>
    /// Returns a 500 Internal Server Error response if there is an unexpected error during processing.
    /// </returns>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Validates the incoming file to ensure it is not null.
    /// 2. Retrieves the user by their ID from the database.
    /// 3. Uploads the new background photo and associates it with the user.
    /// 4. Returns an appropriate response based on the result of the operation.
    ///
    /// Note: This endpoint requires the user to have the "Business" role to access it.
    /// </remarks>
    [Authorize(Roles = "Business")]
    [HttpPatch("{userId}/profile/background-photo")]
    public async Task<ActionResult> ChangeBackgroundPhoto([FromRoute] Guid userId, IFormFile photo)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var fileUploadData = new FileUploadData
        {
            FileType = photo.ContentType,
            Stream = photo.OpenReadStream(),
        };
        
        var user = await _userService.FindById(userId);
        await _userService.ChangeBackgroundPhoto(fileUploadData, user);
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("{userId}/profile")]
    public async Task<ActionResult> ModifyUserProfile()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        return Ok();
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
    [HttpPost("token/refresh")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshJwtDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _refreshJwtDtoValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
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
    [HttpPatch("password")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _changePasswordDtoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var result = await _userService.ChangePassword(request);
        return result ? Ok("Password was successfully changed") : BadRequest("Error");
    }
    
    /// <summary>
    /// Requests a password reset by generating a reset token and sending a reset link to the user's email.
    /// </summary>
    /// <param name="email">The email address of the user who has requested a password reset.</param>
    /// <returns>An action result indicating the success of the password reset request.</returns>
    [HttpPost("password/reset/request")]
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

        await _emailService.SendEmailAsync(new EmailMetadata(user.Email!, 
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

        var validationResult = await _resetPasswordDtoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var user = await _userService.FindByEmail(request.Email);
        var result = await _userService.ResetPassword(user, request.Token, request.NewPassword);

        if (result.Succeeded)
        {
            return Ok("Password has been successfully reset.");
        }

        return BadRequest("Password reset failed. The token may be invalid or expired.");
    }
    
    /// <summary>
    /// Initiates an email change request by generating a confirmation token and sending a confirmation email.
    /// </summary>
    /// <param name="request">The request containing the current email and the new email address.</param>
    /// <returns>An action result indicating the success of the email change request.</returns>
    [Authorize]
    [HttpPost("email")]
    public async Task<ActionResult> RequestEmailChange([FromBody] ChangeEmailDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _changeEmailDtoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var user = await _userService.FindByEmail(request.Email);
        var token = await _userService.GenerateChangeEmailToken(user, request.NewEmail);

        var confirmationLink = Url.Action(
            nameof(ChangeEmail),
            "Account",
            new { userId = user.Id, newEmail = request.NewEmail, token },
            Request.Scheme); 

        await _emailService.SendEmailAsync(new EmailMetadata
            (
                toAddress: user.Email!,
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
    [HttpPatch("email")]
    public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailConfirmationDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var validationResult = await _changeEmailConfirmationDtoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var user = await _userService.FindById(request.UserId);
        var result = await _userService.ChangeEmail(user, request.NewEmail, request.Token);

        if (result.Succeeded)
        {
            return Ok("Email successfully changed.");
        }

        return BadRequest("Unable to confirm email change. The link may have expired or is invalid.");
    }
    
    /// <summary>
    /// Deletes a user account after validating the provided credentials.
    /// </summary>
    /// <param name="request">An object containing the user's credentials (username and password) for authentication.</param>
    /// <returns>
    /// Returns a 204 No Content response if the user was successfully deleted.<br/>
    /// Returns a 400 Bad Request response if the provided credentials are invalid or if validation fails.<br/>
    /// Returns a 404 Not Found response if the user with the specified username does not exist.<br/>
    /// Returns a 500 Internal Server Error response if there is an unexpected error during processing.
    /// </returns>
    /// <remarks>
    /// This method performs the following operations:
    /// 1. Validates the incoming request to ensure it contains valid credentials.
    /// 2. Retrieves the user based on the provided username.
    /// 3. Verifies the password and deletes the user account if the credentials are valid.
    /// 4. Returns an appropriate response based on the result of the operation.
    ///
    /// Note: The user must be authenticated to access this endpoint, and the credentials must match the user's account information.
    /// </remarks>
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteUser([FromBody] LoginDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var validationResult = await _loginDtoValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var identityUser = await _userService.FindByEmail(request.UserName);
        await _userService.DeleteUser(identityUser, request.Password);
        return NoContent();
    }
}

