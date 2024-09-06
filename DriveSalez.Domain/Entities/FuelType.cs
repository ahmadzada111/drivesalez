namespace DriveSalez.Domain.Entities;

public class FuelType
{
    public int Id { get; set; }

    public string Type { get; set; }
    
    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];   
}