using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.RepositoryContracts;

public interface IDetailsRepository
{
    Task<IEnumerable<VehicleColor>> GetAllColorsFromDbAsync();

    Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesFromDbAsync();
        
    Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsFromDbAsync();
        
    Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesFromDbAsync();
        
    Task<IEnumerable<Make>> GetAllMakesFromDbAsync();
        
    Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesFromDbAsync();
        
    Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsFromDbAsync();
        
    Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsFromDbAsync();
        
    Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsFromDbAsync();

    Task<IEnumerable<Model>> GetAllModelsFromDbAsync();

    Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDbAsync(int id);

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDbAsync();

    Task<IEnumerable<Country>> GetAllCountriesFromDbAsync();

    Task<IEnumerable<City>> GetAllCitiesFromDbAsync();

    Task<IEnumerable<Currency>> GetAllCurrenciesFromDbAsync();

    Task<IEnumerable<Subscription>> GetAllSubscriptionsFromDbAsync();
}