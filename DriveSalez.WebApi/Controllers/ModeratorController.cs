using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : Controller
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpPatch("confirm-announcement/{announcementId}")]
        public async Task<ActionResult<Announcement>> ConfirmAnnouncement([FromRoute] int announcementId)
        {
            var response = await _moderatorService.ChangeAnnouncementState(announcementId, AnnouncementState.Active);
            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("make-announcement-inactive/{announcementId}")]
        public async Task<ActionResult<Announcement>> MakeAnnouncementInactive([FromRoute] int announcementId)
        {
            var response = await _moderatorService.ChangeAnnouncementState(announcementId, AnnouncementState.Inactive);
            return response != null ? Ok(response) : BadRequest(response);
        }
        
        [HttpPatch("make-announcement-waiting/{announcementId}")]
        public async Task<ActionResult<Announcement>> MakeAnnouncementWaiting([FromRoute] int announcementId)
        {
            var response = await _moderatorService.ChangeAnnouncementState(announcementId, AnnouncementState.Waiting);
            return response != null ? Ok(response) : BadRequest(response);
        }
    }
}
