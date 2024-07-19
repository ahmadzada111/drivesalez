using DriveSalez.Application.ServiceContracts;
using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.RepositoryContracts;

namespace DriveSalez.Application.Services;

public class DetailsService : IDetailsService
{
    private readonly IDetailsRepository _detailsRepository;
    
    public DetailsService(IDetailsRepository detailsRepository)
    {
        _detailsRepository = detailsRepository;
    }

    public async Task<IEnumerable<VehicleColor>> GetAllColorsAsync()
    {
        var response = await _detailsRepository.GetAllColorsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Make>> GetAllMakesAsync()
    {
        var response = await _detailsRepository.GetAllMakesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
    {
        var response = await _detailsRepository.GetAllSubscriptionsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<AnnouncementTypePricing>> GetAllAnnouncementPricingsAsync()
    {
        var response = await _detailsRepository.GetAllAnnouncementTypePricingsFromDbAsync();
        return response;
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesByCountryIdAsync(int countryId)
    {
        var response = await _detailsRepository.GetAllCitiesByCountryIdFromDbAsync(countryId);
        return response;
    }

    public async Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Model>> GetAllModelsAsync()
    {
        var response = await _detailsRepository.GetAllModelsFromDbAsync();
        return response;
    }
    
    public async Task<IEnumerable<Model>> GetAllModelsByMakeIdAsync(int id)
    {
        var response = await _detailsRepository.GetAllModelsByMakeIdFromDbAsync(id);
        return response;
    }
    
    public async Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsAsync()
    {
        var response = await _detailsRepository.GetAllManufactureYearsFromDbAsync();
        return response;
    }
    
    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var response = await _detailsRepository.GetAllCountriesFromDbAsync();
        return response;
    }
    
    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        var response = await _detailsRepository.GetAllCitiesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
    {
        var response = await _detailsRepository.GetAllCurrenciesFromDbAsync();
        return response;
    }
}