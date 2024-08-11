using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities;

public class ManufactureYear
{
    public int Id { get; set; }
    
    public int Year { get; set; }

    public List<VehicleDetail> VehicleDetails { get; } = [];
}