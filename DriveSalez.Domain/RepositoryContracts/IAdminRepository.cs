using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IAdminRepository
{
    Task<Color> SendNewColorToDbAsync(string color);

    Task<Condition> SendNewVehicleConditionToDbAsync(string condition, string description);

    Task<MarketVersion> SendNewVehicleMarketVersionToDbAsync(string marketVersion);

    Task<Option> SendNewVehicleOptionToDbAsync(string option);

    Task<PricingOption> SendNewSubscriptionToDbAsync(string subscriptionName, decimal price);
        
    Task<Color> UpdateVehicleColorInDbAsync(int colorId, string newColor);

    Task<AccountLimit> UpdateAccountLimitInDbAsync(int limitId, int premiumLimit, int regularLimit);

    Task<PricingOption> UpdateSubscriptionInDbAsync(int subscriptionId, decimal price);

    Task<Condition> UpdateVehicleConditionInDbAsync(int vehicleConditionId, string newVehicleCondition, string newDescription);

    Task<Option> UpdateVehicleOptionInDbAsync(int vehicleOptionId, string newVehicleOption);

    Task<MarketVersion> UpdateVehicleMarketVersionInDbAsync(int marketVersionId, string newMarketVersion);
        
    Task<Color?> DeleteVehicleColorFromDbAsync(int colorId);

    Task<BodyType?> DeleteVehicleBodyTypeFromDbAsync(int bodyTypeId);
        
    Task<DrivetrainType?> DeleteVehicleDrivetrainTypeFromDbAsync(int driveTrainId);

    Task<GearboxType?> DeleteVehicleGearboxTypeFromDbAsync(int gearboxId);

    Task<PricingOption?> DeleteSubscriptionFromDbAsync(int subscriptionId);

    Task<Make?> DeleteMakeFromDbAsync(int makeId);

    Task<Model?> DeleteModelFromDbAsync(int modelId);

    Task<City?> DeleteCityFromDbAsync(int cityId);
        
    Task<Country?> DeleteCountryFromDbAsync(int countryId);
        
    Task<FuelType?> DeleteFuelTypeFromDbAsync(int fuelTypeId);

    Task<Condition?> DeleteVehicleConditionFromDbAsync(int vehicleConditionId);

    Task<Option?> DeleteVehicleOptionFromDbAsync(int vehicleOptionId);

    Task<MarketVersion?> DeleteVehicleMarketVersionFromDbAsync(int marketVersionId);

    Task<ApplicationUser?> DeleteModeratorFromDbAsync(Guid moderatorId);

    Task<PaginatedList<ApplicationUser>> GetAllUsersFromDbAsync(PagingParameters pagingParameters);

    Task<bool> BanUserInDbAsync(Guid userId);
        
    Task<bool> UnbanUserInDbAsync(Guid userId);
}