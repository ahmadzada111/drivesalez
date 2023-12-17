using DriveSalez.Core.DTO;

namespace DriveSalez.Core.RepositoryContracts;

public interface IModeratorRepository
{
    Task<AnnouncementResponseDto> MakeAnnouncementActiveInDbAsync(Guid userId, Guid announcementId);
    
    Task<AnnouncementResponseDto> MakeAnnouncementInactiveInDbAsync(Guid userId, Guid announcementId);

    Task<AnnouncementResponseDto> MakeAnnouncementWaitingInDbAsync(Guid userId, Guid announcementId);
}