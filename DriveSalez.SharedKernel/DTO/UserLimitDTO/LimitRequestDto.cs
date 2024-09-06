namespace DriveSalez.SharedKernel.DTO.UserLimitDTO;

public record LimitRequestDto
{
    public int PremiumLimit { get; init; }
    
    public int RegularLimit { get; init; }
    
    public decimal AccountBalance { get; init; }
}