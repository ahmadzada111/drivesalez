using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IModeratorRepository
{
    Task<Announcement?> MakeAnnouncementActiveInDbAsync(ApplicationUser user, Guid announcementId);
    
    Task<Announcement?> MakeAnnouncementInactiveInDbAsync(ApplicationUser user, Guid announcementId);

    Task<Announcement?> MakeAnnouncementWaitingInDbAsync(ApplicationUser user, Guid announcementId);
}