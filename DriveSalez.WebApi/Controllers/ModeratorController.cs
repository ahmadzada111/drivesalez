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
        private readonly IAnnouncementService _announcementService;

        public ModeratorController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPatch("confirm-announcement/{announcementId}")]
        public async Task<ActionResult<Announcement>> ConfirmAnnouncement([FromRoute] int announcementId)
        {
            var response = await _announcementService.ChangeAnnouncementState(announcementId, AnnouncementState.Active);
            return response != null ? Ok(response) : BadRequest(response);
        }
    }
}
