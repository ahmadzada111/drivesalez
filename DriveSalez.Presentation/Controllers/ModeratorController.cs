using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin, Moderator")]
public class ModeratorController : Controller
{
    private readonly IModeratorService _moderatorService;
    private readonly ILogger _logger;
    
    public ModeratorController(IModeratorService moderatorService, ILogger<ModeratorController> logger)
    {
        _moderatorService = moderatorService;
        _logger = logger;
    }

    [HttpPatch("confirm-announcement/{announcementId}")]
    public async Task<ActionResult<Announcement>> ConfirmAnnouncement([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        try
        {
            var response = await _moderatorService.MakeAnnouncementActiveAsync(announcementId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPatch("make-announcement-inactive/{announcementId}")]
    public async Task<ActionResult<Announcement>> MakeAnnouncementInactive([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        try
        {
            var response = await _moderatorService.MakeAnnouncementInactiveAsync(announcementId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpPatch("make-announcement-waiting/{announcementId}")]
    public async Task<ActionResult<Announcement>> MakeAnnouncementWaiting([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        try
        {
            var response = await _moderatorService.MakeAnnouncementWaitingAsync(announcementId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Problem(e.Message);
        }
    }
}

