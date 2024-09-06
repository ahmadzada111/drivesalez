namespace DriveSalez.Domain.Entities;

public class Country
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<City> Cities { get; set; } = [];

    public ICollection<Announcement> Announcements { get; set; } = [];
}