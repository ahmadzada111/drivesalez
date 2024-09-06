using DriveSalez.SharedKernel.DTO.CityDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface ICityService
{
    Task<GetCityDto> CreateCity(CreateCityDto cityDto);

    Task<IEnumerable<GetCityDto>> GetCities();

    Task<GetCityDto> FindCityById(int id);

    Task<GetCityDto> UpdateCity(UpdateCityDto cityDto);
    
    Task<bool> DeleteCity(int id);
}