namespace DriveSalez.SharedKernel.DTO.ConditionDTO;

public record CreateConditionDto
{
    public string Title { get; init; }
    
    public string Description { get; init; }
}