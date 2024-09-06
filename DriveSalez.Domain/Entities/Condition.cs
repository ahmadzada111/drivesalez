namespace DriveSalez.Domain.Entities;

public class Condition
{
    public int Id { get; set; }
        
    public string Title { get; set; }
        
    public string Description { get; set; }
    
    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];
}