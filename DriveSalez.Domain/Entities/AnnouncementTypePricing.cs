namespace DriveSalez.Domain.Entities;

public class AnnouncementTypePricing
{
    public int Id { get; set; }
    
    public string PricingName { get; set; }

    public SubscriptionPrice Price { get; set; }
}