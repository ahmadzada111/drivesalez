using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<AnnouncementResponseDto?> CreateAnnouncementAsync(CreateAnnouncementDto createAnnouncement);

    Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request);

    Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> GetAnnouncementByIdAsync(Guid id);

    Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdAsync(Guid id);
    
    Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncements(PagingParameters parameters);

    Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters);

    Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserAsync(PagingParameters pagingParameters, AnnouncementState announcementState);

    Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserAsync(PagingParameters pagingParameters);

    Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllPremiumAnnouncementsAsync(PagingParameters pagingParameters);

    Task<LimitRequestDto> GetUserLimitsAsync();

    Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelAsync(PagingParameters parameters, AnnouncementState announcementState);
}