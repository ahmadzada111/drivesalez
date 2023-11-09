using System.ComponentModel;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        
        [HttpPost("login-default-account")]
        public async Task<ActionResult<DefaultUserAuthenticationResponseDto>> LoginDefaultAccount([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.LoginDefaultAccountAsync(request);

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
                return Forbid(e.Message);
            }
        }

        [HttpPost("login-paid-account")]
        public async Task<ActionResult<DefaultUserAuthenticationResponseDto>> LoginPaidAccount([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.LoginPaidAccountAsync(request);

                if (response == null)
                {
                    return Unauthorized("Email or password is invalid");
                }

                return response != null ? Ok(response): BadRequest();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EmailNotConfirmedException e)
            {
                return Forbid(e.Message);
            }
        }
        
        [HttpGet("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return NoContent();
        }
        
        [HttpPost("refresh-default-account")]
        public async Task<ActionResult<DefaultUserAuthenticationResponseDto>> Refresh([FromBody] RefreshJwtDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.RefreshDefaultAccountAsync(request);
                return Ok(response);
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpPost("refresh-paid-account")]
        public async Task<ActionResult<DefaultUserAuthenticationResponseDto>> RefreshPaid([FromBody] RefreshJwtDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            try
            {
                var response = await _accountService.RefreshPaidAccountAsync(request);
                return Ok(response);
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }
        
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
        
        [HttpDelete("delete-user")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser([FromBody] string password)
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
    }
}
