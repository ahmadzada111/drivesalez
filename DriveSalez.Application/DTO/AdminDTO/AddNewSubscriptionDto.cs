namespace DriveSalez.Application.DTO;

public record AddNewSubscriptionDto
{
    public string SubscriptionName { get; init; }
    
    public decimal Price { get; init; } 
    
    public int CurrencyId { get; init; }
}