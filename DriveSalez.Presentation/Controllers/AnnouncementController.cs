using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.DTO.AnnoucementDTO;
using DriveSalez.Application.DTO.AnnouncementDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnnouncementController : Controller
{
    private readonly IAnnouncementService _announcementService;
    private readonly ILogger _logger;
    
    public AnnouncementController(IAnnouncementService announcementService, ILogger<AnnouncementController> logger)
    {
        _announcementService = announcementService;
        _logger = logger;
    }

    [HttpPost("create-announcement")]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto createAnnouncement)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }
        
        var response = await _announcementService.CreateAnnouncementAsync(createAnnouncement);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [HttpGet("get-user-limit")]
    public async Task<ActionResult> GetUserLimits()
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetUserLimitsAsync();
        return Ok(response);
    }
    
    [HttpPatch("update-announcement/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> UpdateAnnouncement([FromBody] UpdateAnnouncementDto createAnnouncement, [FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.UpdateAnnouncementAsync(announcementId, createAnnouncement);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [HttpGet("get-announcement-by-id/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetAnnouncementById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAnnouncementByIdAsync(announcementId);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [AllowAnonymous]
    [HttpGet("get-active-announcement-by-id/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetActiveAnnouncementById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetActiveAnnouncementByIdAsync(announcementId);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [HttpDelete("delete-announcement/{announcementId}")]
    public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.DeleteInactivateAnnouncementAsync(announcementId);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("get-all-inactive-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllInactiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllAnnouncementsForAdminPanelAsync(parameters, AnnouncementState.Inactive);
        return Ok(response);   
    }

    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("get-all-waiting-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllWaitingAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllAnnouncementsForAdminPanelAsync(parameters, AnnouncementState.Pending);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all-active-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllActiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllActiveAnnouncements(parameters);
        return Ok(response);
    }
    
    [HttpPost("reactivate-announcement/{announcementId}")]
    public async Task<ActionResult> ReactivateAnnouncement([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.MakeAnnouncementActiveAsync(announcementId);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPost("make-announcement-inactive/{announcementId}")]
    public async Task<ActionResult> MakeAnnouncementInactiveByUserId([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.MakeAnnouncementInactiveAsync(announcementId);
        return response != null ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("get-all-active-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncementsByUserId([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAnnouncementsByStatesAndByUserAsync(pagingParameters, AnnouncementState.Active);
        return Ok(response);
    }
    
    [HttpGet("get-all-inactive-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>?> GetAllInactiveAnnouncementsByUserId([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAnnouncementsByStatesAndByUserAsync(pagingParameters, AnnouncementState.Inactive);
        return Ok(response);
    }
    
    [HttpGet("get-all-waiting-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllWaitingAnnouncementsByUserId([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAnnouncementsByStatesAndByUserAsync(pagingParameters, AnnouncementState.Pending);
        return Ok(response);
    }
    
    [HttpGet("get-all-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllAnnouncementsByUserId([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncementsByUserAsync(pagingParameters);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("filter-announcements")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> FilterAnnouncements([FromQuery] FilterAnnouncementsRequestDto request)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetFilteredAnnouncementsAsync(request.FilterParameters, request.PagingParameters);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all-premium-announcements")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllPremiumAnnouncements([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllPremiumAnnouncementsAsync(pagingParameters);
        return Ok(response);
    }
}