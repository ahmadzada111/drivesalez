using DriveSalez.Application.DTO;

namespace DriveSalez.Application.ServiceContracts;

public interface IModeratorService
{
    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId);
}