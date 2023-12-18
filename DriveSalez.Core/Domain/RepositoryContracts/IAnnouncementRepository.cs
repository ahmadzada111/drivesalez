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
        
        Task<AnnouncementResponseDto> GetAnnouncementByIdFromDbAsync(Guid id);

        Task<AnnouncementResponseDto> GetActiveAnnouncementByIdFromDbAsync(Guid id);
        
        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsFromDbAsync(PagingParameters parameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsFromDbAsync(
            FilterParameters filterParameters, PagingParameters pagingParameters);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByUserIdFromDbAsync(Guid userId, PagingParameters pagingParameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserIdFromDbAsync(Guid userId,
            PagingParameters pagingParameters);

        Task<LimitRequestDto> GetUserLimitsFromDbAsync(Guid userId);
    }
}
