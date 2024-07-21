using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleOption
{
    public int Id { get; set; }

    public string Option { get; set; }
    
    [JsonIgnore]
    public List<VehicleDetails> Details { get; set; }
}