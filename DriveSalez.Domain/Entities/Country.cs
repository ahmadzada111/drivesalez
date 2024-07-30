using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class Country
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<City> Cities { get; set; }

    [JsonIgnore]
    public List<Announcement> Announcements { get; } = [];
}