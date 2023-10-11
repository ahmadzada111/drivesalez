using Azure;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSalez.WebApi.Controllers;

[Route("api/[controller]")]
public class AnnouncementController : Controller
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpPost("CreateAnnouncement")]
    public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDto announcement)
    {
        var response = await _announcementService.AddAnnouncement(announcement);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpPatch("UpdateAnnouncement/{announcementId}")]
    public async Task<IActionResult> UpdateAnnouncement([FromBody] AnnouncementDto announcement, [FromRoute] int announcementId)
    {
        var response = await _announcementService.UpdateAnnouncement(announcementId, announcement);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpGet("GetAnnouncementById/{announcementId}")]
    public async Task<IActionResult> GetAnnouncementById([FromRoute] int announcementId)
    {
        var response = await _announcementService.GetAnnouncementById(announcementId);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("DeleteAnnouncement/{announcementId}")]
    public async Task<IActionResult> DeleteAnnouncement([FromRoute] int announcementId)
    {
        var response = await _announcementService.DeleteDeactivateAnnouncement(announcementId);
        return response != null ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("GetAnnouncements")]
    public ActionResult<IEnumerable<Announcement>> GetAnnouncements([FromQuery] PagingParameters parameters, AnnouncementState announcementState)
    {
        var response = _announcementService.GetAnnouncements(parameters, announcementState);
        return response != null ? Ok(response) : BadRequest(response);
    }
}