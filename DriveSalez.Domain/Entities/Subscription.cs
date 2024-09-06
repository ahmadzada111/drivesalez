namespace DriveSalez.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }

    public string Name { get; set; } 

    public decimal Price { get; set; }

    public int DurationInDays { get; set; } 

    public ICollection<UserSubscription> UserSubscriptions { get; set; } = [];
}