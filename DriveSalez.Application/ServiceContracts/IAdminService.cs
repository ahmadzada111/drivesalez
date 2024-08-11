using DriveSalez.Application.DTO;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Application.ServiceContracts;

public interface IAdminService
{
    Task<Color?> AddColorAsync(string color);

    Task<PricingOption?> AddSubscriptionAsync(string subscriptionName, decimal price);

    Task<Condition?> AddVehicleConditionAsync(string condition, string description);

    Task<MarketVersion?> AddVehicleMarketVersionAsync(string marketVersion);

    Task<Option?> AddVehicleOptionAsync(string option);

    Task<Color?> UpdateVehicleColorAsync(int colorId, string newColor);

    Task<AccountLimit?> UpdateAccountLimitAsync(int limitId, int premiumLimit, int regularLimit);

    Task<PricingOption?> UpdateSubscriptionAsync(int subscriptionId, decimal price);

    Task<Condition?> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition, string newDescription);
        
    Task<Option?> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption);

    Task<MarketVersion?> UpdateVehicleMarketVersionAsync(int marketVersionId, string newMarketVersion);

    Task<Color?> DeleteVehicleColorAsync(int colorId);

    Task<BodyType?> DeleteVehicleBodyTypeAsync(int bodyTypeId);

    Task<DrivetrainType?> DeleteVehicleDrivetrainTypeAsync(int drivetrainId);

    Task<GearboxType?> DeleteVehicleGearboxTypeAsync(int gearboxId);

    Task<Make?> DeleteMakeAsync(int makeId);

    Task<Model?> DeleteModelAsync(int modelId);

    Task<FuelType?> DeleteFuelTypeAsync(int fuelTypeId);

    Task<Condition?> DeleteVehicleConditionAsync(int vehicleConditionId);

    Task<Option?> DeleteVehicleOptionAsync(int vehicleOptionId);

    Task<Country?> DeleteCountryAsync(int countryId);

    Task<City?> DeleteCityAsync(int cityId);
        
    Task<MarketVersion?> DeleteVehicleMarketVersionAsync(int marketVersionId);
        
    Task<RegisterModeratorResponseDto?> AddModeratorAsync(RegisterModeratorDto registerDto);
        
    Task<PaginatedList<GetModeratorDto>> GetAllModeratorsAsync(PagingParameters pagingParameters);

    Task<GetModeratorDto?> DeleteModeratorAsync(Guid moderatorId);

    Task<PaginatedList<GetUserDto>> GetAllUsers(PagingParameters pagingParameters);

    Task<bool> SendEmailFromStaffAsync(EmailMetadata emailMetadata);

    Task<bool> BanUserAsync(Guid userId);
        
    Task<bool> UnbanUserAsync(Guid userId);
}