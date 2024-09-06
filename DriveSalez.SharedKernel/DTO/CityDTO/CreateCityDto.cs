namespace DriveSalez.SharedKernel.DTO.CityDTO;

public record CreateCityDto
{
    public string Name { get; init; }
    
    public int CountryId { get; init; }
}