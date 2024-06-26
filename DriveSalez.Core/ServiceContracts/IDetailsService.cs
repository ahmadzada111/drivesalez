using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Domain.Entities.VehicleParts;

namespace DriveSalez.Core.ServiceContracts;

public interface IDetailsService
{
    Task<IEnumerable<VehicleColor>> GetAllColorsAsync();

    Task<IEnumerable<VehicleBodyType>> GetAllVehicleBodyTypesAsync();

    Task<IEnumerable<VehicleDrivetrainType>> GetAllVehicleDrivetrainsAsync();

    Task<IEnumerable<VehicleGearboxType>> GetAllVehicleGearboxTypesAsync();

    Task<IEnumerable<Make>> GetAllMakesAsync();

    Task<IEnumerable<AnnouncementTypePricing>> GetAllAnnouncementPricingsAsync();

    Task<IEnumerable<Model>> GetAllModelsByMakeIdAsync(int id);

    Task<IEnumerable<VehicleFuelType>> GetAllVehicleFuelTypesAsync();

    Task<IEnumerable<VehicleCondition>> GetAllVehicleDetailsConditionsAsync();

    Task<IEnumerable<VehicleMarketVersion>> GetAllVehicleMarketVersionsAsync();

    Task<IEnumerable<Model>> GetAllModelsAsync();

    Task<IEnumerable<VehicleOption>> GetAllVehicleDetailsOptionsAsync();

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsAsync();

    Task<IEnumerable<Country>> GetAllCountriesAsync();

    Task<IEnumerable<City>> GetAllCitiesAsync();
    
    Task<IEnumerable<Currency>> GetAllCurrenciesAsync();

    Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
    
    Task<IEnumerable<City>> GetAllCitiesByCountryIdAsync(int countryId);
}