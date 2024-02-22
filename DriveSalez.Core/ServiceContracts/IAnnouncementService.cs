using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<AnnouncementResponseDto?> AddAnnouncementAsync(CreateAnnouncementDto createAnnouncement);

    Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request);

    Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> GetAnnouncementByIdAsync(Guid id);

    Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdAsync(Guid id);
    
    Task<IEnumerable<AnnouncementResponseMiniDto>?> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState);

    Task<IEnumerable<AnnouncementResponseMiniDto>?> GetFilteredAnnouncementsAsync(FilterParameters filterParameters,
        PagingParameters pagingParameters);

    Task<IEnumerable<AnnouncementResponseMiniDto>?> GetAnnouncementsByUserIdAsync(PagingParameters pagingParameters, AnnouncementState announcementState);

    Task<IEnumerable<AnnouncementResponseMiniDto>?> GetAllAnnouncementsByUserIdAsync(PagingParameters pagingParameters);

    Task<LimitRequestDto?> GetUserLimitsAsync();
}