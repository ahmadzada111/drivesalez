namespace DriveSalez.Core.DTO;

public class AddNewSubscriptionDto
{
    public string SubscriptionName { get; set; }
    
    public decimal Price { get; set; } 
    
    public int CurrencyId { get; set; }
}