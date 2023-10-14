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

    public IEnumerable<VehicleColor> GetAllColors()
    {
        var response = _detailsRepository.GetAllColorsFromDb();
        return response;
    }

    public IEnumerable<VehicleBodyType> GetAllVehicleBodyTypes()
    {
        var response = _detailsRepository.GetAllVehicleBodyTypesFromDb();
        return response;
    }

    public IEnumerable<VehicleDrivetrainType> GetAllVehicleDrivetrains()
    {
        var response = _detailsRepository.GetAllVehicleDrivetrainsFromDb();
        return response;
    }

    public IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypes()
    {
        var response = _detailsRepository.GetAllVehicleGearboxTypesFromDb();
        return response;
    }

    public IEnumerable<Make> GetAllMakes()
    {
        var response = _detailsRepository.GetAllMakesFromDb();
        return response;
    }

    public IEnumerable<VehicleFuelType> GetAllVehicleFuelTypes()
    {
        var response = _detailsRepository.GetAllVehicleFuelTypesFromDb();
        return response;
    }

    public IEnumerable<VehicleCondition> GetAllVehicleDetailsConditions()
    {
        var response = _detailsRepository.GetAllVehicleDetailsConditionsFromDb();
        return response;
    }

    public IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersions()
    {
        var response = _detailsRepository.GetAllVehicleMarketVersionsFromDb();
        return response;
    }

    public IEnumerable<VehicleOption> GetAllVehicleDetailsOptions()
    {
        var response = _detailsRepository.GetAllVehicleDetailsOptionsFromDb();
        return response;
    }

    public IEnumerable<Model> GetAllModels()
    {
        var response = _detailsRepository.GetAllModelsFromDb();
        return response;
    }
    
    public IEnumerable<Model> GetAllModelsByMakeId(int id)
    {
        var response = _detailsRepository.GetAllModelsByMakeIdFromDb(id);
        return response;
    }
    
    public IEnumerable<ManufactureYear> GetAllManufactureYears()
    {
        var response = _detailsRepository.GetAllManufactureYearsFromDb();
        return response;
    }
    
    public IEnumerable<Country> GetAllCountries()
    {
        var response = _detailsRepository.GetAllCountriesFromDb();
        return response;
    }
    
    public IEnumerable<City> GetAllCities()
    {
        var response = _detailsRepository.GetAllCitiesFromDb();
        return response;
    }
}