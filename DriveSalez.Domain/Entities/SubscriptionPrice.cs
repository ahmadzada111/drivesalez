namespace DriveSalez.Domain.Entities;

public class SubscriptionPrice
{
    public int Id { get; set; }
    
    public decimal Price { get; set; }
    
    public Currency Currency { get; set; }
}