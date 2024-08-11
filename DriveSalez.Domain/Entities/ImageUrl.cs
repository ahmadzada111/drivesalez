using System.Text.Json.Serialization;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class ImageUrl
{
    public int Id { get; set; }

    public Uri Url { get; set; }

    public Guid? ProfilePhotoUserId { get; set; }

    public Guid? BackgroundPhotoUserId { get; set; }

    public Guid? AnnouncementId { get; set; }

    public Announcement? Announcement{ get; set; }

    public BusinessAccount? ProfilePhotoBusinessAccount { get; set; }
    
    public BusinessAccount? BackgroundPhotoBusinessAccount { get; set; }
}