using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class VehicleDrivetrainType
{
    public int Id { get; set; }

    public string DrivetrainType { get; set; }
    
    public List<VehicleDetails> VehicleDetails { get; set; }        
}