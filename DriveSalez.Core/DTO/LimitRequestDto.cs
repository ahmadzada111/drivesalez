namespace DriveSalez.Core.DTO;

public class LimitRequestDto
{
    public int PremiumLimit { get; set; }
    
    public int RegularLimit { get; set; }
    
    public decimal AccountBalance { get; set; }
}