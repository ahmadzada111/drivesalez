using DriveSalez.Core.DTO;

namespace DriveSalez.Core.ServiceContracts;

public interface IModeratorService
{
    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId);
}