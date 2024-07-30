using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities;

public class ManufactureYear
{
    public int Id { get; set; }
    
    public int Year { get; set; }

    [JsonIgnore]
    public List<VehicleDetail> VehicleDetails { get; } = [];
}