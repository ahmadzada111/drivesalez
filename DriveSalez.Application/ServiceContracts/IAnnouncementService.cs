using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.DTO.AnnoucementDTO;
using DriveSalez.Application.DTO.AnnouncementDTO;
using DriveSalez.Domain.Enums;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Application.ServiceContracts;

public interface IAnnouncementService
{
    Task<AnnouncementResponseDto?> CreateAnnouncementAsync(CreateAnnouncementDto createAnnouncement);

    Task<AnnouncementResponseDto?> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request);

    Task<AnnouncementResponseDto?> DeleteInactivateAnnouncementAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> GetAnnouncementByIdAsync(Guid id);

    Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdAsync(Guid id);

    Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, PaginatedList<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncements(PagingParameters parameters);

    Task<PaginatedList<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters, PagingParameters pagingParameters);

    Task<PaginatedList<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserAsync(PagingParameters pagingParameters, AnnouncementState announcementState);

    Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserAsync(PagingParameters pagingParameters);

    Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllPremiumAnnouncementsAsync(PagingParameters pagingParameters);

    Task<LimitRequestDto> GetUserLimitsAsync();

    Task<PaginatedList<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelAsync(PagingParameters parameters, AnnouncementState announcementState);
}