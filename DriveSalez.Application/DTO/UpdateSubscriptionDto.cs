namespace DriveSalez.Application.DTO;

public class UpdateSubscriptionDto
{
    public int SubscriptionId { get; set; }
    
    public decimal Price { get; set; }
    
    public int CurrencyId { get; set; }
}