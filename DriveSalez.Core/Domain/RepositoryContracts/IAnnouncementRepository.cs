using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementResponseDto> CreateAnnouncementAsync(Guid userId, CreateAnnouncementDto request);

        Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(Guid userId, Guid annoucementId, UpdateAnnouncementDto request);

        Task<AnnouncementResponseDto> DeleteInactiveAnnouncementFromDbAsync(Guid userId, Guid announcementId);

        Task<AnnouncementResponseDto> MakeAnnouncementActiveInDbAsync(Guid userId, Guid annoucementId);

        Task<AnnouncementResponseDto> MakeAnnouncementInactiveInDbAsync(Guid userId, Guid annoucementId);

        Task<AnnouncementResponseDto> MakeAnnouncementWaitingInDbAsync(Guid userId, Guid annoucementId);
        
        Task<AnnouncementResponseDto> GetAnnouncementByIdFromDb(Guid id);

        Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsFromDb(PagingParameters parameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsFromDbAsync(
            FilterParameters filterParameters, PagingParameters pagingParameters);

        Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsByUserIdFromDbAsync(Guid userId, PagingParameters pagingParameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseDto>> GetAllAnnouncementsByUserIdFromDbAsync(Guid userId,
            PagingParameters pagingParameters);

        Task<LimitRequestDto> GetUserLimitsFromDbAsync(Guid userId);
    }
}
