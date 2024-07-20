using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;

namespace DriveSalez.Application.ServiceContracts;

public interface IAdminService
{
    Task<VehicleColor?> AddColorAsync(string color);

    Task<VehicleBodyType?> AddBodyTypeAsync(string bodyType);

    Task<Subscription?> AddSubscriptionAsync(string subscriptionName, decimal price, int currencyId);

    Task<Country?> AddCountryAsync(string country);

    Task<City?> AddCityAsync(string city, int countryId);
        
    Task<Currency?> AddCurrencyAsync(string currencyName);
        
    Task<VehicleDrivetrainType?> AddVehicleDrivetrainTypeAsync(string driveTrainType);

    Task<VehicleGearboxType?> AddVehicleGearboxTypeAsync(string gearboxType);

    Task<Make?> AddMakeAsync(string make);

    Task<Model?> AddModelAsync(int makeId, string model);

    Task<VehicleFuelType?> AddVehicleFuelTypeAsync(string fuelType);

    Task<VehicleCondition?> AddVehicleConditionAsync(string condition, string description);

    Task<VehicleMarketVersion?> AddVehicleMarketVersionAsync(string marketVersion);

    Task<VehicleOption?> AddVehicleOptionAsync(string option);

    Task<VehicleColor?> UpdateVehicleColorAsync(int colorId, string newColor);

    Task<VehicleBodyType?> UpdateVehicleBodyTypeAsync(int bodyTypeId, string newBodyType);

    Task<VehicleDrivetrainType?> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain);

    Task<VehicleGearboxType?> UpdateVehicleGearboxTypeAsync(int gearboxId, string newGearbox);

    Task<AccountLimit?> UpdateAccountLimitAsync(int limitId, int premiumLimit, int regularLimit);

    Task<Currency?> UpdateCurrencyAsync(int currencyId, string currencyName);

    Task<Subscription?> UpdateSubscriptionAsync(int subscriptionId, decimal price, int currencyId);
        
    Task<Make?> UpdateMakeAsync(int makeId, string newMake);

    Task<Model?> UpdateModelAsync(int modelId, string newModel);

    Task<VehicleFuelType?> UpdateFuelTypeAsync(int fuelTypeId, string newFuelType);

    Task<VehicleCondition?> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition, string newDescription);

    Task<Country?> UpdateCountryAsync(int countryId, string newCountry);

    Task<City?> UpdateCityAsync(int cityId, string newCity);
        
    Task<VehicleOption?> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption);

    Task<VehicleMarketVersion?> UpdateVehicleMarketVersionAsync(int marketVersionId, string newMarketVersion);

    Task<VehicleColor?> DeleteVehicleColorAsync(int colorId);

    Task<VehicleBodyType?> DeleteVehicleBodyTypeAsync(int bodyTypeId);

    Task<VehicleDrivetrainType?> DeleteVehicleDrivetrainTypeAsync(int drivetrainId);

    Task<VehicleGearboxType?> DeleteVehicleGearboxTypeAsync(int gearboxId);

    Task<Make?> DeleteMakeAsync(int makeId);

    Task<Model?> DeleteModelAsync(int modelId);

    Task<VehicleFuelType?> DeleteFuelTypeAsync(int fuelTypeId);

    Task<VehicleCondition?> DeleteVehicleConditionAsync(int vehicleConditionId);

    Task<VehicleOption?> DeleteVehicleOptionAsync(int vehicleOptionId);

    Task<Currency?> DeleteCurrencyAsync(int currencyId);

    Task<Country?> DeleteCountryAsync(int countryId);

    Task<City?> DeleteCityAsync(int cityId);
        
    Task<VehicleMarketVersion?> DeleteVehicleMarketVersionAsync(int marketVersionId);
        
    Task<RegisterModeratorResponseDto?> AddModeratorAsync(RegisterModeratorDto registerDto);
        
    Task<IEnumerable<GetModeratorDto>> GetAllModeratorsAsync();

    Task<GetModeratorDto?> DeleteModeratorAsync(Guid moderatorId);

    Task<IEnumerable<GetUserDto>> GetAllUsers();

    Task<bool> SendEmailFromStaffAsync(string mail, string subject, string body);

    Task<bool> BanUserAsync(Guid userId);
        
    Task<bool> UnbanUserAsync(Guid userId);
}