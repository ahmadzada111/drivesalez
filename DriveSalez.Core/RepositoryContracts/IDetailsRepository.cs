using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.RepositoryContracts;

public interface IDetailsRepository
{
    Task<IEnumerable<VehicleColor>> GetAllColorsFromDb();

    Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesFromDb();
        
    Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsFromDb();
        
    Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesFromDb();
        
    Task<IEnumerable<Make>> GetAllMakesFromDb();
        
    Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesFromDb();
        
    Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsFromDb();
        
    Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsFromDb();
        
    Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsFromDb();

    Task<IEnumerable<Model>> GetAllModelsFromDb();

    Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDb(int id);

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDb();

    Task<IEnumerable<Country>> GetAllCountriesFromDb();

    Task<IEnumerable<City>> GetAllCitiesFromDb();

}