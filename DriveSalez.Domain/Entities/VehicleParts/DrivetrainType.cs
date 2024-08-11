using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class DrivetrainType
{
    public int Id { get; set; }

    public string Type { get; set; }
    
    public List<VehicleDetail> VehicleDetails { get; } = [];     
}