using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleMarketVersion
{
    public int Id { get; set; }

    public string MarketVersion { get; set; }
    
    [JsonIgnore]
    public List<VehicleDetails> VehicleDetails { get; set; }        
}