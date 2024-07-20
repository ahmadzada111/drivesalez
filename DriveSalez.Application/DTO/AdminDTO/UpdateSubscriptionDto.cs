namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateSubscriptionDto
{
    public int SubscriptionId { get; init; }
    
    public decimal Price { get; init; }
    
    public int CurrencyId { get; init; }
}