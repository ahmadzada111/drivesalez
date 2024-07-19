namespace DriveSalez.Application.DTO;

public class UpdateUserLimitDto
{
    public int LimitId { get; set; } 
    
    public int PremiumLimit { get; set; }
    
    public int RegularLimit { get; set; }
}