namespace DriveSalez.Domain.Entities;

public class Model
{
    public int Id { get; set; }
        
    public string Title { get; set; }

    public int MakeId { get; set; }

    public Make Make { get; set; }
    
    public List<Vehicle> Vehicles { get; } = [];
}