using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }

    [JsonIgnore]
    public int MakeId { get; set; }

    [JsonIgnore]
    public int ModelId { get; set; }

    public Make Make { get; set; }

    public Model Model { get; set; }

    public VehicleDetail VehicleDetail { get; set; }

    [JsonIgnore]
    public List<Announcement> Announcements { get; } = [];
}