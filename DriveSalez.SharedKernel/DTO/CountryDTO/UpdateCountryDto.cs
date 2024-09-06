namespace DriveSalez.SharedKernel.DTO.CountryDTO;

public record UpdateCountryDto
{
    public int Id { get; init; }
    
    public string Name { get; init; }
}