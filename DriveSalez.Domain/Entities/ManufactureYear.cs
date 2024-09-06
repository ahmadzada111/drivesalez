namespace DriveSalez.Domain.Entities;

public class ManufactureYear
{
    public int Id { get; set; }
    
    public int Year { get; set; }

    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];
}