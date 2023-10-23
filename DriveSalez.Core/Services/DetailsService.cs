using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;

namespace DriveSalez.Core.Services;

public class DetailsService : IDetailsService
{
    private readonly IDetailsRepository _detailsRepository;
    
    public DetailsService(IDetailsRepository detailsRepository)
    {
        _detailsRepository = detailsRepository;
    }

    public async Task<IEnumerable<VehicleColor>> GetAllColors()
    {
        var response = await _detailsRepository.GetAllColorsFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypes()
    {
        var response = await _detailsRepository.GetAllVehicleBodyTypesFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrains()
    {
        var response = await _detailsRepository.GetAllVehicleDrivetrainsFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypes()
    {
        var response = await _detailsRepository.GetAllVehicleGearboxTypesFromDb();
        return response;
    }

    public async Task<IEnumerable<Make>> GetAllMakes()
    {
        var response = await _detailsRepository.GetAllMakesFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypes()
    {
        var response = await _detailsRepository.GetAllVehicleFuelTypesFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditions()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsConditionsFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersions()
    {
        var response = await _detailsRepository.GetAllVehicleMarketVersionsFromDb();
        return response;
    }

    public async Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptions()
    {
        var response = await _detailsRepository.GetAllVehicleDetailsOptionsFromDb();
        return response;
    }

    public async Task<IEnumerable<Model>> GetAllModels()
    {
        var response = await _detailsRepository.GetAllModelsFromDb();
        return response;
    }
    
    public async Task<IEnumerable<Model>> GetAllModelsByMakeId(int id)
    {
        var response = await _detailsRepository.GetAllModelsByMakeIdFromDb(id);
        return response;
    }
    
    public async Task<IEnumerable<ManufactureYear>> GetAllManufactureYears()
    {
        var response = await _detailsRepository.GetAllManufactureYearsFromDb();
        return response;
    }
    
    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        var response = await _detailsRepository.GetAllCountriesFromDb();
        return response;
    }
    
    public async Task<IEnumerable<City>> GetAllCities()
    {
        var response = await _detailsRepository.GetAllCitiesFromDb();
        return response;
    }
}