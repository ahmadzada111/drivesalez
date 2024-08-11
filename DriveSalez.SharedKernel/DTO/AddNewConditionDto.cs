namespace DriveSalez.Application.DTO;

public record AddNewConditionDto
{
    public string Condition { get; init; }
    
    public string Description { get; init; }
}