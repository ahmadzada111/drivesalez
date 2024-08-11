using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities.VehicleParts;

public class MarketVersion
{
    public int Id { get; set; }

    public string Version { get; set; }
    
    public List<VehicleDetail> VehicleDetails { get; } = [];     
}