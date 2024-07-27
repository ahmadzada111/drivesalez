using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAdminRepository
{
    Task<VehicleColor> SendNewColorToDbAsync(string color);

    Task<VehicleBodyType> SendNewBodyTypeToDbAsync(string bodyType);

    Task<City> SendNewCityToDbAsync(string city, int countryId);
        
    Task<Country> SendNewCountryToDbAsync(string country);
        
    Task<VehicleDrivetrainType> SendNewVehicleDrivetrainTypeToDbAsync(string driveTrainType);

    Task<VehicleGearboxType> SendNewVehicleGearboxTypeToDbAsync(string gearboxType);

    Task<Make> SendNewMakeToDbAsync(string make);

    Task<Model> SendNewModelToDbAsync(int makeId, string model);

    Task<VehicleFuelType> SendNewVehicleFuelTypeToDbAsync(string fuelType);

    Task<VehicleCondition> SendNewVehicleConditionToDbAsync(string condition, string description);

    Task<VehicleMarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion);

    Task<VehicleOption> SendNewVehicleOptionToDbAsync(string option);

    Task<Subscription> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price, int currencyId);
        
    Task<Currency> SendNewCurrencyToDbAsync(string currencyName);
        
    Task<VehicleColor> UpdateVehicleColorInDbAsync(int colorId, string newColor);

    Task<VehicleBodyType> UpdateVehicleBodyTypeInDbAsync(int bodyTypeId, string newBodyType);

    Task<AccountLimit> UpdateAccountLimitInDbAsync(int limitId, int premiumLimit, int regularLimit);

    Task<City> UpdateCityInDbAsync(int cityId, string newCity);
        
    Task<Currency> UpdateCurrencyInDbAsync(int currencyId, string currencyName);

    Task<Subscription> UpdateSubscriptionInDbAsync(int subscriptionId, decimal price, int currencyId);
        
    Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeInDbAsync(int driveTrainId, string newDrivetrain);

    Task<VehicleGearboxType> UpdateVehicleGearboxTypeInDbAsync(int gearboxId, string newGearbox);

    Task<Make> UpdateMakeInDbAsync(int gearboxId, string newGearbox);

    Task<Country> UpdateCountryInDbAsync(int countryId, string newCountry);
        
    Task<Model> UpdateModelInDbAsync(int modelId, string newModel);

    Task<VehicleFuelType> UpdateFuelTypeInDbAsync(int fuelTypeId, string newFuelType);

    Task<VehicleCondition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition, string newDescription);

    Task<VehicleOption> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption);

    Task<VehicleMarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion);
        
    Task<VehicleColor?> DeleteVehicleColorFromDbAsync(int colorId);

    Task<VehicleBodyType?> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId);

    Task<Currency?> DeleteCurrencyFromDbAsync(int currencyId);
        
    Task<VehicleDrivetrainType?> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId);

    Task<VehicleGearboxType?> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId);

    Task<Subscription?> DeleteSubscriptionFromDbAsync(int subscriptionId);

    Task<Make?> DeleteMakeFromDbAsync(int makeId);

    Task<Model?> DeleteModelFromDbAsync(int modelId);

    Task<City?> DeleteCityFromDbAsync(int cityId);
        
    Task<Country?> DeleteCountryFromDbAsync(int countryId);
        
    Task<VehicleFuelType?> DeleteFuelTypeFromDbAsync(int fuelTypeId);

    Task<VehicleCondition?> DeleteVehicleConditionFromDbAsync(int vehicleConditionId);

    Task<VehicleOption?> DeleteVehicleOptionFromDbAsync(int vehicleOptionId);

    Task<VehicleMarketVersion?> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId);

    Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId);

    Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters);

    Task<bool> BanUserInDbAsync(Guid userId);
        
    Task<bool> UnbanUserInDbAsync(Guid userId);
}