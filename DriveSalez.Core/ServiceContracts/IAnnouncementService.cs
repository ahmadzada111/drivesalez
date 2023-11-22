using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<AnnouncementResponseDto> AddAnnouncementAsync(CreateAnnouncementDto createAnnouncement);

    Task<AnnouncementResponseDto> UpdateAnnouncementAsync(Guid announcementId, UpdateAnnouncementDto request);

    Task<AnnouncementResponseDto> DeleteDeactivateAnnouncementAsync(Guid announcementId);

    Task<AnnouncementResponseDto> ChangeAnnouncementStateAsync(Guid announcementId, AnnouncementState announcementState);

    Task<AnnouncementResponseDto> GetAnnouncementByIdAsync(Guid id);

    Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState);

    Task<IEnumerable<AnnouncementResponseDto>> GetFilteredAnnouncementsAsync(FilterParameters filterParameters,
        PagingParameters pagingParameters);

    Task<IEnumerable<AnnouncementResponseDto>> GetAnnouncementsByUserIdAsync(PagingParameters pagingParameters, AnnouncementState announcementState);

    Task<IEnumerable<AnnouncementResponseDto>> GetAllAnnouncementsByUserIdAsync(PagingParameters pagingParameters);

    Task<int> GetUserLimitsAsync();
}