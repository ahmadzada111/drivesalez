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
    }

    [HttpPatch("update-announcement/{announcementId}")]
    public async Task<IActionResult> UpdateAnnouncement([FromBody] CreateAnnouncementDto createAnnouncement, [FromRoute] Guid announcementId)
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
    public IActionResult GetAnnouncementById([FromRoute] Guid announcementId)
    {
        try
        {
            var response =  _announcementService.GetAnnouncementById(announcementId);
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
    public IActionResult GetAnnouncements([FromQuery] PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = _announcementService.GetAnnouncements(parameters, announcementState);
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
    
    // [HttpGet("filter-announcements")]
    // public Task<ActionResult> FilterAnnouncements([FromRoute] FilterParameters parameters)
    // {
    //     
    // }
}