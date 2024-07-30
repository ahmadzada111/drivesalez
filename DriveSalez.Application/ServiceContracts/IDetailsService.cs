using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.ServiceContracts;

public interface IDetailsService
{
    Task<IEnumerable<Color>> GetAllColorsAsync();

    Task<IEnumerable<BodyType>> GetAllVehicleBodyTypesAsync();

    Task<IEnumerable<DrivetrainType>> GetAllVehicleDrivetrainsAsync();

    Task<IEnumerable<GearboxType>> GetAllVehicleGearboxTypesAsync();

    Task<IEnumerable<Make>> GetAllMakesAsync();

    Task<IEnumerable<AnnouncementTypePricing>> GetAllAnnouncementPricingsAsync();

    Task<IEnumerable<Model>> GetAllModelsByMakeIdAsync(int id);

    Task<IEnumerable<FuelType>> GetAllVehicleFuelTypesAsync();

    Task<IEnumerable<Condition>> GetAllVehicleDetailsConditionsAsync();

    Task<IEnumerable<MarketVersion>> GetAllVehicleMarketVersionsAsync();

    Task<IEnumerable<Model>> GetAllModelsAsync();

    Task<IEnumerable<Option>> GetAllVehicleDetailsOptionsAsync();

    Task<IEnumerable<ManufactureYear>> GetAllManufactureYearsAsync();

    Task<IEnumerable<Country>> GetAllCountriesAsync();

    Task<IEnumerable<City>> GetAllCitiesAsync();

    Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
    
    Task<IEnumerable<City>> GetAllCitiesByCountryIdAsync(int countryId);
}