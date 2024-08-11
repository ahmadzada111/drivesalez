using DriveSalez.Application.DTO;

namespace DriveSalez.Application.ServiceContracts;

public interface IDetailsService
{
    Task<IEnumerable<ColorDto>> GetAllColorsAsync();

    Task<IEnumerable<BodyTypeDto>> GetAllVehicleBodyTypesAsync();

    Task<IEnumerable<DrivetrainTypeDto>> GetAllVehicleDrivetrainsAsync();

    Task<IEnumerable<GearboxTypeDto>> GetAllVehicleGearboxTypesAsync();

    Task<IEnumerable<MakeDto>> GetAllMakesAsync();

    Task<IEnumerable<AnnouncementTypePricingDto>> GetAllAnnouncementPricingsAsync();

    Task<IEnumerable<ModelDto>> GetAllModelsByMakeIdAsync(int id);

    Task<IEnumerable<FuelTypeDto>> GetAllVehicleFuelTypesAsync();

    Task<IEnumerable<ConditionDto>> GetAllVehicleDetailsConditionsAsync();

    Task<IEnumerable<MarketVersionDto>> GetAllVehicleMarketVersionsAsync();

    Task<IEnumerable<ModelDto>> GetAllModelsAsync();

    Task<IEnumerable<OptionDto>> GetAllVehicleDetailsOptionsAsync();

    Task<IEnumerable<ManufactureYearDto>> GetAllManufactureYearsAsync();

    Task<IEnumerable<CountryDto>> GetAllCountriesAsync();

    Task<IEnumerable<CityDto>> GetAllCitiesAsync();

    Task<IEnumerable<AnnouncementTypePricingDto>> GetAllSubscriptionsAsync();
    
    Task<IEnumerable<CityDto>> GetAllCitiesByCountryIdAsync(int countryId);
}