using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class VehicleCondition
{
    public int Id { get; set; }
        
    public string Condition { get; set; }
        
    public string Description { get; set; }
    
    public List<VehicleDetails> VehicleDetails { get; set; }
}