namespace DriveSalez.SharedKernel.DTO.UserLimitDTO;

public record CreateUserLimitDto
{
    public Guid UserId { get; set; }
    
    public string LimitType { get; set; }
    
    public int LimitValue { get; set; }
}