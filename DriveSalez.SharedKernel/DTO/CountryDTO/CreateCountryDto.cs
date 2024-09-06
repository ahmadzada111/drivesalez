namespace DriveSalez.SharedKernel.DTO.CountryDTO;

public record CreateCountryDto
{
    public string Name { get; init; }
    
    public List<string> Cities { get; init; }
}