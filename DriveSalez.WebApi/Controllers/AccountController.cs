using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponseDto>> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var response = await _accountService.Register(request);

            if (response.Error != null)
            {
                return BadRequest(response.Error);
            }
            
            return Ok(response);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDto>> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Unauthorized(errorMessage);
            }

            var responce = await _accountService.Login(request);

            if (responce == null)
            {
                return Unauthorized("Email or password is invalid");
            }

            return Ok(responce);
        }

        [HttpGet("logout")]
        public async Task<ActionResult<AuthenticationResponseDto>> LogOut()
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

            var response = await _accountService.Refresh(request);

            if (response.Error != null)
            {
                return Unauthorized(response.Error);
            }

            return Ok(response);
        }
        
        [HttpDelete("delete-user")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser([FromBody] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Unauthorized("Password is invalid");
            }

            var response = await _accountService.DeleteUser(password);
            return response != null ? Ok() : BadRequest();
        }
    }
}
