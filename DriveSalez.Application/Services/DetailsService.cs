using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.RepositoryContracts;

namespace DriveSalez.Application.Services;

public class DetailsService : IDetailsService
{
    private readonly IDetailsRepository _detailsRepository;
    
    public DetailsService(IDetailsRepository detailsRepository)
    {
        _detailsRepository = detailsRepository;
    }

    public async Task<IEnumerable<Color>> GetAllColorsAsync()
    {
        var response = await _detailsRepository.GetAllColorsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<BodyType>> GetAllVehicleBodyTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<DrivetrainType>> GetAllVehicleDrivetrainsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<GearboxType>> GetAllVehicleGearboxTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Make>> GetAllMakesAsync()
    {
        var response = await _detailsRepository.GetAllMakesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<FuelType>> GetAllVehicleFuelTypesAsync()
    {
        var response = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Condition>> GetAllVehicleDetailsConditionsAsync()
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

    public async Task<IEnumerable<MarketVersion>> GetAllVehicleMarketVersionsAsync()
    {
        var response = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();
        return response;
    }

    public async Task<IEnumerable<Option>> GetAllVehicleDetailsOptionsAsync()
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
}