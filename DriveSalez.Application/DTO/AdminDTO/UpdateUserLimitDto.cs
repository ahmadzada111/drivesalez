namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateUserLimitDto
{
    public int LimitId { get; init; } 
    
    public int PremiumLimit { get; init; }
    
    public int RegularLimit { get; init; }
}