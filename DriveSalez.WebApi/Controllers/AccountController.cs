using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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

        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register([FromBody] RegisterDto request)
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
        
        [HttpPost("Login")]
        public async Task<ActionResult<ApplicationUser>> Login([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var responce = await _accountService.Login(request);

            if (responce == null)
            {
                return BadRequest("Email or password is invalid");
            }

            return Ok(responce);
        }

        [HttpGet("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
        
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshJwtDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }

            var response = await _accountService.Refresh(request);

            if (response.Error != null)
            {
                return BadRequest(response.Error);
            }

            return Ok(response);
        }
    }
}
