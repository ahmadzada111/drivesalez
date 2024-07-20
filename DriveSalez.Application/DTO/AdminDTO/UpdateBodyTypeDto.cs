namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateBodyTypeDto
{
    public int BodyTypeId { get; init; }
    
    public string NewBodyType { get; init; }
}