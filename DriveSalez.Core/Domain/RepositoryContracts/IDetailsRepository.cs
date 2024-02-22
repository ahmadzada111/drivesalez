using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.Entities.VehicleParts;

namespace DriveSalez.Core.Domain.RepositoryContracts;

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

    Task<IEnumerable<AnnouncementTypePricing>> GetAllAnnouncementTypePricingsFromDbAsync();

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDbAsync();

    Task<IEnumerable<Country>> GetAllCountriesFromDbAsync();

    Task<IEnumerable<City>> GetAllCitiesFromDbAsync();

    Task<IEnumerable<Currency>> GetAllCurrenciesFromDbAsync();

    Task<IEnumerable<Subscription>> GetAllSubscriptionsFromDbAsync();

    Task<IEnumerable<City>> GetAllCitiesByCountryIdFromDbAsync(int countryId);
}