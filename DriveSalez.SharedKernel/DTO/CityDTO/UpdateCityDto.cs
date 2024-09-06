namespace DriveSalez.SharedKernel.DTO.CityDTO;

public record UpdateCityDto
{
    public int Id { get; init; }
    
    public string Name { get; init; }
}