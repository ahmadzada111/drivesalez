namespace DriveSalez.Domain.Entities;

public class Make
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public ICollection<Model> Models { get; set; } = [];

    public ICollection<Vehicle> Vehicles { get; set; } = [];
}