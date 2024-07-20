using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleOption
{
    public int Id { get; set; }

    public string Option { get; set; }
    
    public List<VehicleDetails> Details { get; set; }
}