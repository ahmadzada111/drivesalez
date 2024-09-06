namespace DriveSalez.Domain.Entities;

public class Option
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public ICollection<VehicleDetail> VehicleDetails { get; set; } = [];
}