namespace DriveSalez.Domain.Entities;

public class Make
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public List<Model> Models { get; } = [];

    public List<Vehicle> Vehicles { get; } = [];
}