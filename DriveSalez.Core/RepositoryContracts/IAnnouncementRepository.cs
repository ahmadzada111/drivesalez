using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> CreateAnnouncementAsync(Guid userId, AnnouncementDto request);

        Task<Announcement> UpdateAnnouncementInDbAsync(Guid userId, int annoucementId, AnnouncementDto request);

        Task<Announcement> DeleteInactiveAnnouncementFromDbAsync(Guid userId, int announcementId);

        Task<Announcement> ChangeAnnouncementStateInDbAsync(Guid userId, int annoucementId, AnnouncementState announcementState);

        Announcement GetAnnouncementByIdFromDb(int id);

        IEnumerable<Announcement> GetAnnouncementsFromDb(PagingParameters parameters, AnnouncementState announcementState);

        Task<IEnumerable<Announcement>> GetFilteredAnnouncementsFromDbAsync(FilterParameters parameters);
    }
}
