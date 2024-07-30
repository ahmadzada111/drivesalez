using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class Make
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    [JsonIgnore]
    public List<Vehicle> Vehicles { get; } = [];
}