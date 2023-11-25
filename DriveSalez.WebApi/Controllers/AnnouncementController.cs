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

    [AllowAnonymous]
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
    
    [AllowAnonymous]
    [HttpGet("get-announcements")]
    public async Task<ActionResult<AnnouncementResponseDto>> GetAnnouncements([FromQuery] PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = await _announcementService.GetAnnouncements(parameters, announcementState);
        return response != null ? Ok(response) : BadRequest();
    }

    [HttpPost("reactivate-announcement/{announcementId}")]
    public async Task<ActionResult> ReactivateAnnouncement([FromRoute] Guid announcementId)
    {
        try
        {
            var response = await _announcementService.ChangeAnnouncementStateAsync(announcementId, AnnouncementState.Active);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpGet("get-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseDto>>> GetAnnouncementsByUserId(PagingParameters pagingParameters, AnnouncementState announcementState)
    {
        try
        {
            var response = await _announcementService.GetAnnouncementsByUserIdAsync(pagingParameters, announcementState);
            return response != null ? Ok(response) : BadRequest(response);
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    [HttpGet("get-all-announcements-by-user-id")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseDto>>> GetAllAnnouncementsByUserId(PagingParameters pagingParameters)
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
    
    [HttpGet("filter-announcements")]
    public async Task<ActionResult<IEnumerable<AnnouncementResponseDto>>> FilterAnnouncements(FilterParameters filterParameters, PagingParameters pagingParameters)
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