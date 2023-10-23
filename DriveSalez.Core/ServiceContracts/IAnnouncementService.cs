using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<Announcement> AddAnnouncement(AnnouncementDto announcement);

    Task<Announcement> UpdateAnnouncement(int announcementId, AnnouncementDto request);

    Task<Announcement> DeleteDeactivateAnnouncement(int announcementId);

    Task<Announcement> ChangeAnnouncementState(int announcementId, AnnouncementState announcementState);

    Announcement GetAnnouncementById(int id);

    IEnumerable<Announcement> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState);
}