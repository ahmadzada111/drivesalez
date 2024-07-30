using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class City
{
    public int Id { get; set; } 

    public string Name { get; set; }

    public int CountryId { get; set; }    

    [JsonIgnore]
    public Country Country { get; set; }    

    [JsonIgnore]
    public List<Announcement> Announcements { get; } = [];
}