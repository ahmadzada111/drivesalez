namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateCountryDto
{
    public int CountryId { get; init; }
    
    public string NewCountry { get; init; }
}