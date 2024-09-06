namespace DriveSalez.SharedKernel.DTO;

public record UpdateSubscriptionDto
{
    public int SubscriptionId { get; init; }
    
    public decimal Price { get; init; }
}