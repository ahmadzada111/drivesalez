namespace DriveSalez.SharedKernel.DTO.ConditionDTO;

public record ConditionDto
{
    public int Id { get; init; }
        
    public string Title { get; init; }
        
    public string Description { get; init; }
}