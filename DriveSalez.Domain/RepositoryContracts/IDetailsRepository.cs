using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IDetailsRepository
{
    Task<IEnumerable<Color>> GetAllColorsFromDbAsync();

    Task<IEnumerable<BodyType>> GetAllVehicleBodyTypesFromDbAsync();
        
    Task<IEnumerable<DrivetrainType>> GetAllVehicleDrivetrainsFromDbAsync();
        
    Task<IEnumerable<GearboxType>> GetAllVehicleGearboxTypesFromDbAsync();
        
    Task<IEnumerable<Make>> GetAllMakesFromDbAsync();
        
    Task<IEnumerable<FuelType>> GetAllVehicleFuelTypesFromDbAsync();
        
    Task<IEnumerable<Condition>> GetAllVehicleDetailsConditionsFromDbAsync();
        
    Task<IEnumerable<MarketVersion>> GetAllVehicleMarketVersionsFromDbAsync();
        
    Task<IEnumerable<Option>> GetAllVehicleDetailsOptionsFromDbAsync();

    Task<IEnumerable<Model>> GetAllModelsFromDbAsync();

    Task<IEnumerable<Model>> GetAllModelsByMakeIdFromDbAsync(int id);

    Task<IEnumerable<PricingOption>> GetAllAnnouncementTypePricingsFromDbAsync();

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsFromDbAsync();

    Task<IEnumerable<Country>> GetAllCountriesFromDbAsync();

    Task<IEnumerable<City>> GetAllCitiesFromDbAsync();

    Task<IEnumerable<PricingOption>> GetAllSubscriptionsFromDbAsync();

    Task<IEnumerable<City>> GetAllCitiesByCountryIdFromDbAsync(int countryId);
}