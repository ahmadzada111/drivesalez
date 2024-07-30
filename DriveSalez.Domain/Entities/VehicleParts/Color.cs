using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class Color
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    [JsonIgnore]
    public List<VehicleDetail> VehicleDetails { get; } = [];
}