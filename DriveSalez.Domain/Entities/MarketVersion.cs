namespace DriveSalez.Domain.Entities;

public class MarketVersion
{
    public int Id { get; set; }

    public string Version { get; set; }
    
    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];     
}