namespace DriveSalez.SharedKernel.DTO.UserLimitDTO;

public record UpdateAccountLimitDto
{
    public int PremiumLimit { get; init; }
    
    public int RegularLimit { get; init; }
}