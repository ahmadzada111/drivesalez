using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<Announcement> AddAnnouncementAsync(AnnouncementDto announcement);

    Task<Announcement> UpdateAnnouncementAsync(int announcementId, AnnouncementDto request);

    Task<Announcement> DeleteDeactivateAnnouncementAsync(int announcementId);

    Task<Announcement> ChangeAnnouncementStateAsync(int announcementId, AnnouncementState announcementState);

    Announcement GetAnnouncementById(int id);

    IEnumerable<Announcement> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState);
}