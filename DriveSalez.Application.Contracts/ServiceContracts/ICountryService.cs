using DriveSalez.SharedKernel.DTO.CountryDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface ICountryService
{
    Task<GetCountryDto> CreateCountry(string name);

    Task<GetCountryDto> CreateCountry(CreateCountryDto countryDto);
    
    Task<IEnumerable<GetCountryDto>> GetCountries();

    Task<GetCountryDto> FindCountryById(int id);

    Task<GetCountryDto> UpdateCountry(UpdateCountryDto countryDto);
    
    Task<bool> DeleteCountry(int id);
}