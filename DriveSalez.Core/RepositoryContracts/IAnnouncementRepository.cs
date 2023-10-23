using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> CreateAnnouncement(Guid userId, AnnouncementDto request);

        Task<Announcement> UpdateAnnouncementInDb(Guid userId, int annoucementId, AnnouncementDto request);

        Task<Announcement> DeleteInactiveAnnouncementFromDb(Guid userId, int announcementId);

        Task<Announcement> ChangeAnnouncementStateInDb(Guid userId, int annoucementId, AnnouncementState announcementState);

        Announcement GetAnnouncementByIdFromDb(int id);

        IEnumerable<Announcement> GetAnnouncementsFromDb(PagingParameters parameters, AnnouncementState announcementState);
    }
}
