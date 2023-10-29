using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Pagination;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts;

public interface IAnnouncementService
{
    Task<AnnouncementResponseDto> AddAnnouncementAsync(CreateAnnouncementDto createAnnouncement);

    Task<AnnouncementResponseDto> UpdateAnnouncementAsync(int announcementId, CreateAnnouncementDto request);

    Task<AnnouncementResponseDto> DeleteDeactivateAnnouncementAsync(int announcementId);

    Task<AnnouncementResponseDto> ChangeAnnouncementStateAsync(int announcementId, AnnouncementState announcementState);

    AnnouncementResponseDto GetAnnouncementById(int id);

    IEnumerable<AnnouncementResponseDto> GetAnnouncements(PagingParameters parameters, AnnouncementState announcementState);
}