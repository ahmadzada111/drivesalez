using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO;

namespace DriveSalez.Core.Domain.RepositoryContracts;

public interface IModeratorRepository
{
    Task<AnnouncementResponseDto?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementWaitingInDbAsync(ApplicationUser user, Guid announcementId);
}