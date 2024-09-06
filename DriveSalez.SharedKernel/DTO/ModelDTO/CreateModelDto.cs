namespace DriveSalez.SharedKernel.DTO.ModelDTO;

public record CreateModelDto
{
    public int Id { get; init; }
    
    public int ModelId { get; init; }
    
    public string Title { get; init; }
}