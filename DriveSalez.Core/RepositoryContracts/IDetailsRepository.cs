using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.RepositoryContracts;

public interface IDetailsRepository
{
    IEnumerable<VehicleColor> GetAllColorsFromDb();

    IEnumerable<VehicleBodyType> GetAllVehicleBodyTypesFromDb();
        
    IEnumerable<VehicleDrivetrainType> GetAllVehicleDrivetrainsFromDb();
        
    IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypesFromDb();
        
    IEnumerable<Make> GetAllMakesFromDb();
        
    IEnumerable<VehicleFuelType> GetAllVehicleFuelTypesFromDb();
        
    IEnumerable<VehicleCondition> GetAllVehicleDetailsConditionsFromDb();
        
    IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersionsFromDb();
        
    IEnumerable<VehicleOption> GetAllVehicleDetailsOptionsFromDb();

    IEnumerable<Model> GetAllModelsFromDb();

    IEnumerable<Model> GetAllModelsByMakeIdFromDb(int id);

    IEnumerable<ManufactureYear> GetAllManufactureYearsFromDb();

    IEnumerable<Country> GetAllCountriesFromDb();

    IEnumerable<City> GetAllCitiesFromDb();

}