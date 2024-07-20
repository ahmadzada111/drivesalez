using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleGearboxType
{
    public int Id { get; set; }

    public string GearboxType { get; set; }
    
    public List<VehicleDetails> VehicleDetails { get; set; }        
}