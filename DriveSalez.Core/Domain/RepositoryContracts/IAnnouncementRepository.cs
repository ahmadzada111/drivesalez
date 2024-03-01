using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.Domain.RepositoryContracts
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementResponseDto> CreateAnnouncementInDbAsync(ApplicationUser user, CreateAnnouncementDto request);

        Task<AnnouncementResponseDto> UpdateAnnouncementInDbAsync(ApplicationUser user, Guid announcementId, UpdateAnnouncementDto request);

        Task<AnnouncementResponseDto?> DeleteInactiveAnnouncementFromDbAsync(ApplicationUser user, Guid announcementId);

        Task<AnnouncementResponseDto?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId);

        Task<AnnouncementResponseDto?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId);
        
        Task<AnnouncementResponseDto?> GetAnnouncementByIdFromDbAsync(Guid id);

        Task<AnnouncementResponseDto?> GetActiveAnnouncementByIdFromDbAsync(Guid id);
        
        Task<Tuple<IEnumerable<AnnouncementResponseMiniDto>, IEnumerable<AnnouncementResponseMiniDto>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters parameters);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetFilteredAnnouncementsFromDbAsync(
            FilterParameters filterParameters, PagingParameters pagingParameters);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAnnouncementsByStatesAndByUserFromDbAsync(ApplicationUser user, PagingParameters pagingParameters, AnnouncementState announcementState);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsByUserFromDbAsync(ApplicationUser user,
            PagingParameters pagingParameters);

        Task<IEnumerable<AnnouncementResponseMiniDto>> GetAllAnnouncementsForAdminPanelFromDbAsync(PagingParameters parameter, AnnouncementState announcementState);
    }
}
