using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public int MakeId { get; set; }

    public int ModelId { get; set; }

    public Make Make { get; set; }

    public Model Model { get; set; }

    public VehicleDetail VehicleDetail { get; set; }

    public List<Announcement> Announcements { get; } = [];
}