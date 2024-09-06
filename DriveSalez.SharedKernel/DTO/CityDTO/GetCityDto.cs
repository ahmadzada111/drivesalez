namespace DriveSalez.SharedKernel.DTO.CityDTO;

public record GetCityDto
{
    public int Id { get; init; }
    
    public string Name { get; init; }
}