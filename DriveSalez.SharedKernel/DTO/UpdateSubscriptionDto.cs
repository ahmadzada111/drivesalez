namespace DriveSalez.Application.DTO;

public record UpdateSubscriptionDto
{
    public int SubscriptionId { get; init; }
    
    public decimal Price { get; init; }
}