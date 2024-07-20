namespace DriveSalez.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    
    public string SubscriptionName { get; set; }
    
    public SubscriptionPrice Price { get; set; }
}