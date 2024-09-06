// using Asp.Versioning;
// using DriveSalez.Application.Contracts.ServiceContracts;
// using DriveSalez.Persistence.Contracts.ServiceContracts;
// using DriveSalez.SharedKernel.DTO;
// using DriveSalez.SharedKernel.Utilities;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Microsoft.IdentityModel.Tokens;
//
// namespace DriveSalez.Presentation.Controllers;
//
// [ApiController]
// [ApiVersion(1)]
// [Route("api/v{v:apiVersion}/admins")]
// [Authorize(Roles = "Admin")]
// public class AdminController : Controller
// {
//     private readonly IAdminService _adminService;
//     private readonly ILogger _logger;
//     private readonly IEmailService _emailService;
//     
//     public AdminController(IAdminService adminService, ILogger<AdminController> logger,
//         IEmailService emailService)
//     {
//         _adminService = adminService;
//         _logger = logger;
//         _emailService = emailService;
//     }
//
//     [HttpPost("colors")]
//     public async Task<ActionResult> AddColor([FromBody] string color)
//     {
//         _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
//         
//         var response = await _adminService.AddColorAsync(color);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpPost("conditions")]
//     public async Task<ActionResult> AddCondition([FromBody] AddConditionDto request)
//     {
//         var response = await _adminService.AddConditionAsync(request.Condition, request.Description);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//
//     [HttpPost("market-versions")]
//     public async Task<ActionResult> AddMarketVersion([FromBody] string marketVersion)
//     {
//         var response = await _adminService.AddMarketVersionAsync(marketVersion);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//
//     [HttpPost("options")]
//     public async Task<ActionResult> AddOption([FromBody] string option)
//     {
//         var response = await _adminService.AddOptionAsync(option);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//
//     [HttpPut("colors/{colorId}")]
//     public async Task<ActionResult> UpdateColor([FromRoute] int colorId, [FromBody] string newColor)
//     {
//         var response = await _adminService.UpdateColorAsync(colorId, newColor);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpPut("limits/{limitId}")]
//     public async Task<ActionResult> UpdateAccountLimit([FromRoute] int limitId, [FromBody] UpdateAccountLimitDto request)
//     {
//         var response = await _adminService.UpdateAccountLimitAsync(limitId, request.PremiumLimit, request.RegularLimit);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpPut("conditions/{conditionId}")]
//     public async Task<ActionResult> UpdateCondition([FromRoute] int conditionId, [FromBody] UpdateConditionDto request)
//     {
//         var response = await _adminService.UpdateConditionAsync(conditionId, request.NewCondition, request.NewDescription);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpPut("options/{optionId}")]
//     public async Task<ActionResult> UpdateOption([FromRoute] int optionId, [FromBody] string newOptions)
//     {
//         var response = await _adminService.UpdateOptionAsync(optionId, newOptions);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpPut("market-version/{versionId}")]
//     public async Task<ActionResult> UpdateMarketVersion([FromRoute] int versionId, [FromBody] string newVersion)
//     {
//         var response = await _adminService.UpdateMarketVersionAsync(versionId, newVersion);
//         return response != null ? Ok(response) : BadRequest(response);
//     }
//     
//     [HttpDelete("colors/{colorId}")]
//     public async Task<ActionResult> DeleteColor([FromBody] int colorId)
//     {
//         var response = await _adminService.DeleteColorAsync(colorId); 
//         return response != null ? NoContent() : BadRequest(response);
//     }
//     
//     [HttpDelete("conditions/{conditionId}")]
//     public async Task<ActionResult> DeleteCondition([FromBody] int conditionId)
//     {
//         var response = await _adminService.DeleteConditionAsync(conditionId);
//         return response != null ? NoContent() : BadRequest(response);
//     }
//     
//     [HttpDelete("options/{optionId}")]
//     public async Task<ActionResult> DeleteOption([FromBody] int optionId)
//     {
//         var response = await _adminService.DeleteOptionAsync(optionId);
//         return response != null ? NoContent() : BadRequest(response);
//     }
//     
//     [HttpDelete("market-version/{versionId}")]
//     public async Task<ActionResult> DeleteMarketVersion([FromBody] int versionId)
//     {
//         var response = await _adminService.DeleteMarketVersionAsync(versionId);
//         return response != null ? NoContent() : BadRequest(response);
//     }
//
//     [HttpPost("moderator")]
//     public async Task<ActionResult> CreateModerator([FromBody] RegisterModeratorDto request)
//     {
//         if (!ModelState.IsValid)
//         {
//             string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
//             return Problem(errorMessage);
//         }
//         
//         var result = await _adminService.AddModeratorAsync(request);
//         return result != null ? Ok() : BadRequest();
//     }
//
//     [HttpDelete("moderator/{moderatorId}")]
//     public async Task<ActionResult> DeleteModerator([FromBody] Guid moderatorId)
//     {
//         var result = await _adminService.DeleteModeratorAsync(moderatorId);
//         return result != null ? NoContent() : BadRequest(result);
//     }
//     
//     [HttpGet("moderators")]
//     public async Task<ActionResult> GetAllModerators(PagingParameters pagingParameters)
//     {
//         var result = await _adminService.GetAllModeratorsAsync(pagingParameters);
//         return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
//     }
//
//     [HttpGet("get-all-users")]
//     public async Task<ActionResult> GetAllUsers(PagingParameters pagingParameters)
//     {
//         var result = await _adminService.GetAllUsers(pagingParameters);
//         return !result.IsNullOrEmpty() ? Ok(result) : BadRequest();
//     }
//
//     [HttpPost("send-mail-to-user")]
//     public async Task<ActionResult> SendEmail([FromBody] EmailMetadata request)
//     {
//         await _emailService.SendEmailAsync(request);
//         return Ok();
//     }
//
//     [HttpPost("ban-user")]
//     public async Task<ActionResult> BanUser([FromBody] Guid userId)
//     {
//         var result = await _adminService.BanUserAsync(userId);
//
//         if (result)
//         {
//             return Ok();
//         }
//
//         return BadRequest();
//     }
//     
//     [HttpPost("unban-user")]
//     public async Task<ActionResult> UnbanUser([FromBody] Guid userId)
//     {
//         var result = await _adminService.UnbanUserAsync(userId);
//
//         if (result)
//         {
//             return Ok();
//         }
//
//         return BadRequest();
//     }
// }
//
