using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AnnouncementController : Controller
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpPost("create-announcement")]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto createAnnouncement)
    {
        if (!ModelState.IsValid)
        {
            string errorMessage = string.Join(" | ",
                ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        try
        {
            var response = await _announcementService.AddAnnouncementAsync(createAnnouncement);
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (PaymentFailedException e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("get-user-limit")]
    public async Task<ActionResult> GetUserLimits()
    {
        try
        {
            var response = await _announcementService.GetUserLimitsAsync();
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpPatch("update-announcement/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> UpdateAnnouncement([FromBody] UpdateAnnouncementDto createAnnouncement, [FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.UpdateAnnouncementAsync(announcementId, createAnnouncement);
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-announcement-by-id/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetAnnouncementById([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.GetAnnouncementByIdAsync(announcementId);
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpGet("get-active-announcement-by-id/{announcementId}")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetActiveAnnouncementById([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.GetActiveAnnouncementByIdAsync(announcementId);
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpDelete("delete-announcement/{announcementId}")]
    public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.DeleteDeactivateAnnouncementAsync(announcementId);
            return response != null ? Ok(response) : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("get-all-inactive-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllInactiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        var response = await _announcementService.GetAnnouncements(parameters, AnnouncementState.Inactive);
        return response != null ? Ok(response) : BadRequest();
    }

    [Authorize(Roles = "Admin, Moderator")]
    [HttpGet("get-all-waiting-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllWaitingAnnouncements([FromQuery] PagingParameters parameters)
    {
        var response = await _announcementService.GetAnnouncements(parameters, AnnouncementState.Waiting);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [AllowAnonymous]
    [HttpGet("get-all-active-announcements")]
    public async Task<ActionResult<AnnouncementResponseMiniDto>> GetAllActiveAnnouncements([FromQuery] PagingParameters parameters)
    {
        var response = await _announcementService.GetAnnouncements(parameters, AnnouncementState.Active);
        return response != null ? Ok(response) : BadRequest();
    }
    
    [HttpPost("reactivate-announcement/{announcementId}")]
    public async Task<ActionResult> ReactivateAnnouncement([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.MakeAnnouncementActiveAsync(announcementId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpPost("make-announcement-inactive/{announcementId}")]
    public async Task<ActionResult> MakeAnnouncementInactiveByUserId([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.MakeAnnouncementInactiveAsync(announcementId);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-all-active-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncementsByUserId(PagingParameters pagingParameters)
    {
        try
        {
            var response = await _announcementService.GetAnnouncementsByUserIdAsync(pagingParameters, AnnouncementState.Active);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-all-inactive-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllInactiveAnnouncementsByUserId(PagingParameters pagingParameters)
    {
        try
        {
            var response = await _announcementService.GetAnnouncementsByUserIdAsync(pagingParameters, AnnouncementState.Inactive);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-all-waiting-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllWaitingAnnouncementsByUserId(PagingParameters pagingParameters)
    {
        try
        {
            var response = await _announcementService.GetAnnouncementsByUserIdAsync(pagingParameters, AnnouncementState.Waiting);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-all-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> GetAllAnnouncementsByUserId(PagingParameters pagingParameters)
    {
        try
        {
            var response = await _announcementService.GetAllAnnouncementsByUserIdAsync(pagingParameters);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("filter-announcements")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseMiniDto>>> FilterAnnouncements(FilterParameters filterParameters, PagingParameters pagingParameters)
    {
        try
        {
            var response = await _announcementService.GetFilteredAnnouncementsAsync(filterParameters, pagingParameters);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
}