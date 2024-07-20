using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AnnoucementDTO;
using DriveSalez.Application.DTO.AnnouncementDTO;

namespace DriveSalez.Application.ServiceContracts;

public interface IModeratorService
{
    Task<AnnouncementResponseDto?> MakeAnnouncementActiveAsync(Guid announcementId);
    
    Task<AnnouncementResponseDto?> MakeAnnouncementInactiveAsync(Guid announcementId);

    Task<AnnouncementResponseDto?> MakeAnnouncementWaitingAsync(Guid announcementId);
}