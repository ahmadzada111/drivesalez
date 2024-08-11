namespace DriveSalez.Application.DTO;

public record UpdateConditionDto
{
    public int ConditionId { get; init; } 
    
    public string NewCondition { get; init; }
    
    public string NewDescription { get; init; }
}