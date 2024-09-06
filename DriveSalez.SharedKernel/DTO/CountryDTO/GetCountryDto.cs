using DriveSalez.SharedKernel.DTO.CityDTO;

namespace DriveSalez.SharedKernel.DTO.CountryDTO;

public record GetCountryDto
{
    public int Id { get; init; }

    public string Name { get; init; }

    public List<GetCityDto>? Cities { get; init; }
}