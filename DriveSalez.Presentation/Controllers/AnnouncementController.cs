using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Controllers;

/// <summary>
/// Handles operations related to announcements, including creation, updates, retrieval, and deletion.
/// </summary>
[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/announcements")]
[Authorize]
public class AnnouncementController : Controller
{
    private readonly IAnnouncementService _announcementService;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
    /// </summary>
    /// <param name="announcementService">Service to handle announcements.</param>
    /// <param name="logger">Logger for the controller.</param>
    public AnnouncementController(IAnnouncementService announcementService, ILogger<AnnouncementController> logger)
    {
        _announcementService = announcementService;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new announcement.
    /// </summary>
    /// <param name="createAnnouncement">The details of the announcement to create.</param>
    /// <returns>
    /// Returns 200 with the created announcement if successful.<br/>
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto createAnnouncement, List<IFormFile> photos)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var filesData = photos.Select(x => new FileUploadData()
        {
            Stream = x.OpenReadStream(),
            FileType = x.ContentType
        }).ToList();
        
        var response = await _announcementService.CreateAnnouncement(createAnnouncement, filesData);
        return CreatedAtAction(nameof(CreateAnnouncement), new { announcementId = response.Id }, "Announcement created successfully.");
    }
    
    /// <summary>
    /// Updates an existing announcement.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to update.</param>
    /// <param name="updateAnnouncement">The updated details of the announcement.</param>
    /// <returns>
    /// Returns 200 with the updated announcement if successful.<br/>
    /// Returns 400 if the request is invalid or the announcement does not exist.
    /// </returns>
    [HttpPatch("{announcementId}")]
    public async Task<ActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementDto updateAnnouncement, [FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.UpdateAnnouncement(updateAnnouncement, announcementId);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves an announcement by its ID.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to retrieve.</param>
    /// <returns>
    /// Returns 200 with the announcement if found.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [HttpGet("{announcementId}")]
    public async Task<ActionResult> GetAnnouncementById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.FindAnnouncementById(announcementId);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves an active announcement by its ID.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to retrieve.</param>
    /// <returns>
    /// Returns 200 with the active announcement if found.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("active/{announcementId}")]
    public async Task<ActionResult> GetActiveAnnouncementById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.FindAnnouncementById(announcementId);
        return Ok(response);
    }
    
    /// <summary>
    /// Deletes an announcement (inactivate) by its ID.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to delete.</param>
    /// <returns>
    /// Returns 204 if the announcement was successfully deleted.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [HttpDelete("{announcementId}")]
    public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.DeleteAnnouncement(announcementId);
        return response ? NoContent() : BadRequest();
    }
    
    /// <summary>
    /// Retrieves all inactive announcements for the admin panel.
    /// </summary>
    /// <param name="parameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of inactive announcements.
    /// </returns>
    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("active")]
    public async Task<ActionResult> GetAllInactiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllAnnouncements(x => x.AnnouncementState == AnnouncementState.Inactive, parameters);
        return Ok(response);   
    }

    /// <summary>
    /// Retrieves all pending announcements for the admin panel.
    /// </summary>
    /// <param name="parameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of pending announcements.
    /// </returns>
    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("pending")]
    public async Task<ActionResult> GetAllPendingAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllAnnouncements(x => x.AnnouncementState == AnnouncementState.Pending, parameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves all active announcements.
    /// </summary>
    /// <param name="parameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of active announcements.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<ActionResult> GetAllActiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");

        var response = await _announcementService.GetAllAnnouncements(x => x.AnnouncementState == AnnouncementState.Active, parameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Activates an announcement by its ID.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to activate.</param>
    /// <returns>
    /// Returns 200 if the announcement was successfully activated.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [HttpPatch("{announcementId}/activate")]
    public async Task<ActionResult> MakeAnnouncementActiveById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.ChangeAnnouncementState(announcementId, AnnouncementState.Active);
        return Ok(response);
    }

    /// <summary>
    /// Deactivates an announcement by its ID.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to deactivate.</param>
    /// <returns>
    /// Returns 200 if the announcement was successfully deactivated.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [HttpPatch("{announcementId}/deactivate")]
    public async Task<ActionResult> MakeAnnouncementInactiveById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.ChangeAnnouncementState(announcementId, AnnouncementState.Inactive);
        return Ok(response);
    }

    /// <summary>
    /// Marks an announcement as pending.
    /// </summary>
    /// <param name="announcementId">The ID of the announcement to mark as pending.</param>
    /// <returns>
    /// Returns 200 if the announcement was successfully marked as pending.<br/>
    /// Returns 400 if the announcement does not exist.
    /// </returns>
    [Authorize(Roles = "Moderator, Admin")]
    [HttpPatch("{announcementId}/pending")]
    public async Task<ActionResult> MakeAnnouncementPendingById([FromRoute] Guid announcementId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.ChangeAnnouncementState(announcementId, AnnouncementState.Pending);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all active announcements created by the current user.
    /// </summary>
    /// <param name="pagingParameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of active announcements created by the user.
    /// </returns>
    [HttpGet("users/{userId}/active")]
    public async Task<ActionResult> GetAllActiveAnnouncementsByUser([FromQuery] PagingParameters pagingParameters, [FromRoute] Guid userId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncements(x => x.Owner.Id == userId && x.AnnouncementState == AnnouncementState.Active, pagingParameters);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all inactive announcements created by the current user.
    /// </summary>
    /// <param name="pagingParameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of inactive announcements created by the user.
    /// </returns>
    [HttpGet("users/{userId}/inactive")]
    public async Task<ActionResult> GetAllInactiveAnnouncementsByUser([FromQuery] PagingParameters pagingParameters, [FromRoute] Guid userId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncements(x => x.Owner.Id == userId && x.AnnouncementState == AnnouncementState.Inactive, pagingParameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves all pending announcements created by the current user.
    /// </summary>
    /// <param name="pagingParameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of pending announcements created by the user.
    /// </returns>
    [HttpGet("users/{userId}/pending")]
    public async Task<ActionResult> GetAllWaitingAnnouncementsByUser([FromQuery] PagingParameters pagingParameters, [FromRoute] Guid userId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncements(x => x.Owner.Id == userId && x.AnnouncementState == AnnouncementState.Pending, pagingParameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves all announcements created by the current user.
    /// </summary>
    /// <param name="pagingParameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of all announcements created by the user.
    /// </returns>
    [HttpGet("users/{userId}/announcements")]
    public async Task<ActionResult> GetAllAnnouncementsByUser([FromQuery] PagingParameters pagingParameters, [FromRoute] Guid userId)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncements(x => x.Owner.Id == userId, pagingParameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Filters announcements based on the provided criteria.
    /// </summary>
    /// <param name="filterAnnouncementParameters">The filter paging parameters for the announcements.</param>
    /// <param name="pagingParameters">The paging parameters for the announcements.</param>
    /// <returns>
    /// Returns 200 with a list of announcements that match the filter criteria.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("filter")]
    public async Task<ActionResult> FilterAnnouncements([FromQuery] FilterAnnouncementParameters filterAnnouncementParameters, [FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetFilteredAnnouncementsAsync(filterAnnouncementParameters, pagingParameters);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves all premium announcements.
    /// </summary>
    /// <param name="pagingParameters">Utilities parameters for the query.</param>
    /// <returns>
    /// Returns 200 with a list of premium announcements.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("premium")]
    public async Task<ActionResult> GetAllPremiumAnnouncements([FromQuery] PagingParameters pagingParameters)
    {
        _logger.LogInformation($"[{DateTime.Now.ToLongTimeString()}] Path: {HttpContext.Request.Path}");
        
        var response = await _announcementService.GetAllAnnouncements(x => x.IsPremium, pagingParameters);
        return Ok(response);
    }
}