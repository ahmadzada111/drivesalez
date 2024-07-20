using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleColor
{
    public int Id { get; set; }

    public string Color { get; set; }

    [JsonIgnore]
    public List<VehicleDetails> VehicleDetails { get; set; }
}