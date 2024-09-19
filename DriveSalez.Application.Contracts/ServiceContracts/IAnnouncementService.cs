using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IAnnouncementService
{
    Task<GetAnnouncementDto> CreateAnnouncement(CreateAnnouncementDto request, List<FileUploadData> photos);
    
    Task<PaginatedList<GetAnnouncementMiniDto>> GetAllAnnouncements(Expression<Func<Announcement, bool>>? whereExpression, 
        PagingParameters pagingParameters);

    Task<GetAnnouncementDto> FindAnnouncementById(Guid id,  bool incrementViewCount = false);

    Task<GetAnnouncementDto> UpdateAnnouncement(UpdateAnnouncementDto announcementDto, Guid announcementId);
    
    Task<bool> DeleteAnnouncement(Guid id);
    
    Task<GetAnnouncementDto> ChangeAnnouncementState(Guid announcementId, AnnouncementState announcementState);

    Task<PaginatedList<GetAnnouncementMiniDto>> GetFilteredAnnouncementsAsync(FilterAnnouncementParameters filterAnnouncementParameters, PagingParameters pagingParameters);
}