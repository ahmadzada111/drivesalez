using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.ServiceContracts;

public interface IDetailsService
{
    Task<IEnumerable<VehicleColor>> GetAllColors();

    Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypes();

    Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrains();

    Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypes();

    Task<IEnumerable<Make>> GetAllMakes();

    Task<IEnumerable<Model>> GetAllModelsByMakeId(int id);

    Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypes();

    Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditions();

    Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersions();

    Task<IEnumerable<Model>> GetAllModels();

    Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptions();

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYears();

    Task<IEnumerable<Country>> GetAllCountries();

    Task<IEnumerable<City>> GetAllCities();
}