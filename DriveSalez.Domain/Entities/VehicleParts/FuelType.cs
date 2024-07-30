using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class FuelType
{
    public int Id { get; set; }

    public string Type { get; set; }
    
    [JsonIgnore]
    public List<VehicleDetail> VehicleDetails { get; } = [];   
}