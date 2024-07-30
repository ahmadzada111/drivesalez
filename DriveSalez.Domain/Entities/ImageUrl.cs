using System.Text.Json.Serialization;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class ImageUrl
{
    public int Id { get; set; }

    public Uri Url { get; set; }

    [JsonIgnore]
    public Guid? ProfilePhotoUserId { get; set; }

    [JsonIgnore]
    public Guid? BackgroundPhotoUserId { get; set; }

    [JsonIgnore]
    public Guid? AnnouncementId { get; set; }

    [JsonIgnore]
    public Announcement? Announcement{ get; set; }

    [JsonIgnore]
    public BusinessAccount? ProfilePhotoBusinessAccount { get; set; }
    
    [JsonIgnore]
    public BusinessAccount? BackgroundPhotoBusinessAccount { get; set; }
}