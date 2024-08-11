namespace DriveSalez.Domain.Entities.VehicleDetailsFiles;

public class Condition
{
    public int Id { get; set; }
        
    public string Title { get; set; }
        
    public string Description { get; set; }
    
    public List<VehicleDetail> VehicleDetails { get; } = [];
}