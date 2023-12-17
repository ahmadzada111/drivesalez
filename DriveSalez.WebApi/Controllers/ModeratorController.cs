using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator")]
    public class ModeratorController : Controller
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpPatch("confirm-announcement/{announcementId}")]
        public async Task<ActionResult<Announcement>> ConfirmAnnouncement([FromRoute] Guid announcementId)
        {
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
        }

        [HttpPatch("make-announcement-inactive/{announcementId}")]
        public async Task<ActionResult<Announcement>> MakeAnnouncementInactive([FromRoute] Guid announcementId)
        {
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
        }
        
        [HttpPatch("make-announcement-waiting/{announcementId}")]
        public async Task<ActionResult<Announcement>> MakeAnnouncementWaiting([FromRoute] Guid announcementId)
        {
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
        }
    }
}
