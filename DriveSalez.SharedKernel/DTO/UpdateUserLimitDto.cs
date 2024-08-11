namespace DriveSalez.Application.DTO;

public record UpdateAccountLimitDto
{
    public int LimitId { get; init; } 
    
    public int PremiumLimit { get; init; }
    
    public int RegularLimit { get; init; }
}