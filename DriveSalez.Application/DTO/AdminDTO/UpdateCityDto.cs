namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateCityDto
{
    public int CityId { get; init; } 
    
    public string NewCity { get; init; }
}