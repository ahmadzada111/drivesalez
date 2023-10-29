using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;
        
        public AccountController(SignInManager<ApplicationUser> signInManager, IAccountService accountService, 
            IOtpService otpService, IEmailService emailService, IMemoryCache cache)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _otpService = otpService;
            _emailService = emailService;
            _cache = cache;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var response = await _accountService.RegisterAsync(request);

            if (!response.Succeeded)
            {
                return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDto>> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Unauthorized(errorMessage);
            }

            var response = await _accountService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Email or password is invalid");
            }

            return Ok(response);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationResponseDto>> Refresh([FromBody] RefreshJwtDto request)
        {
            if (request == null)
            {
                return Unauthorized("Invalid request");
            }

            var response = await _accountService.RefreshAsync(request);

            if (response.Error != null)
            {
                return Unauthorized(response.Error);
            }

            return Ok(response);
        }
        
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var result = await _accountService.ChangePasswordAsync(request);
            return result ? Ok("Password was successfully changed") : BadRequest("Error");
        }
        
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            var response =  await _otpService.ValidateOtpAsync(_cache, request.ValidateRequest);

            if (response)
            {
                var result = await _emailService.ResetPasswordAsync(request.ValidateRequest.Email, request.NewPassword);
            
                if (result)
                {
                    return Ok("Password was successfully changed");
                }

                return BadRequest("User not found");
            }

            return BadRequest("Cannot validate OTP");
        }
        
        [HttpDelete("delete-user")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser([FromBody] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Unauthorized("Password is invalid");
            }

            var response = await _accountService.DeleteUserAsync(password);
            return response != null ? Ok() : BadRequest();
        }
    }
}
