namespace DriveSalez.Core.DTO;

public class UpdateSubscriptionDto
{
    public int SubscriptionId { get; set; }
    
    public string NewSubscriptionName { get; set; } 
    
    public decimal Price { get; set; }
    
    public int CurrencyId { get; set; }
}