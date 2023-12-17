using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IOtpService _otpService;
        private readonly IMemoryCache _cache;
        
        public AccountController(IAccountService accountService, IOtpService otpService, IMemoryCache cache)
        {
            _accountService = accountService;
            _otpService = otpService;
            _cache = cache;
        }

        [HttpPost("register-default-account")]
        public async Task<ActionResult> RegisterDefaultAccount([FromBody] RegisterDefaultAccountDto request)
        {
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

        [HttpPost("register-premium-account")]
        public async Task<ActionResult> RegisterPremiumAccount([FromBody] RegisterPaidAccountDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var response = await _accountService.RegisterPremiumAccountAsync(request);

            if (!response.Succeeded)
            {
                return BadRequest(string.Join(" | ", response.Errors.Select(e => e.Description)));
            }

            return Ok();
        }
        
        [HttpPost("register-business-account")]
        public async Task<ActionResult> RegisterBusinessAccount([FromBody] RegisterPaidAccountDto request)
        {
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
        
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDto>> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.LoginAsync(request);

                if (response == null)
                {
                    return Unauthorized("Email or password is invalid");
                }

                return Ok(response);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EmailNotConfirmedException e)
            {
                return new ContentResult()
                {
                    StatusCode = 403,
                    Content = e.Message,
                    ContentType = "text/plain"
                };
            }
        }
        
        [HttpPost("login-staff")]
        public async Task<ActionResult<AuthenticationResponseDto>> LoginStaff([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.LoginStaffAsync(request);

                if (response == null)
                {
                    return Unauthorized("Email or password is invalid");
                }

                return Ok(response);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return NoContent();
        }
        
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationResponseDto>> Refresh([FromBody] RefreshJwtDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.RefreshAsync(request);
                return Ok(response);
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var result = await _accountService.ChangePasswordAsync(request);
                return result ? Ok("Password was successfully changed") : BadRequest("Error");
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response =  await _otpService.ValidateOtpAsync(_cache, request.ValidateRequest);

                if (response)
                {
                    var result = await _accountService.ResetPasswordAsync(request.ValidateRequest.Email, request.NewPassword);
            
                    if (result)
                    {
                        return Ok("Password was successfully changed");
                    }
                }

                return BadRequest("Cannot validate OTP");
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [Authorize]
        [HttpPost("change-email")]
        public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response =  await _otpService.ValidateOtpAsync(_cache, request.ValidateRequest);

                if (response)
                {
                    var result = await _accountService.ChangeEmailAsync(request.ValidateRequest.Email, request.NewMail);
            
                    if (result)
                    {
                        return Ok("Password was successfully changed");
                    }
                }

                return BadRequest("Cannot validate OTP");
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [Authorize]
        [HttpDelete("delete-user")]
        public async Task<ActionResult> DeleteUser([FromBody] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Unauthorized("Password is invalid");
            }

            try
            {
                var response = await _accountService.DeleteUserAsync(password);
                return response ? Ok() : BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPost("create-admin")]
        public async Task<ActionResult> CreateAdmin()
        {
            try
            {
                var result = await _accountService.CreateAdminAsync();
                return result ? Ok() : BadRequest();
            }
            catch (UserNotAuthorizedException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
