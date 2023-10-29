using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementResponseDto> CreateAnnouncementAsync(Guid userId, CreateAnnouncementDto request);

        Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(Guid userId, int annoucementId, CreateAnnouncementDto request);

        Task<AnnouncementResponseDto> DeleteInactiveAnnouncementFromDbAsync(Guid userId, int announcementId);

        Task<AnnouncementResponseDto> ChangeAnnouncementStateInDbAsync(Guid userId, int annoucementId, AnnouncementState announcementState);

        AnnouncementResponseDto GetAnnouncementByIdFromDb(int id);

        IEnumerable<AnnouncementResponseDto> GetAnnouncementsFromDb(PagingParameters parameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsFromDbAsync(FilterParameters parameters);
    }
}
