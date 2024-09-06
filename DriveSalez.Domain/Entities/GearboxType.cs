namespace DriveSalez.Domain.Entities;

public class GearboxType
{
    public int Id { get; set; }

    public string Type { get; set; }
    
    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];
}