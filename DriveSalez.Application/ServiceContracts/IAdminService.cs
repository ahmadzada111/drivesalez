using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Application.ServiceContracts;

public interface IAdminService
{
    Task<Color?> AddColorAsync(string color);

    Task<BodyType?> AddBodyTypeAsync(string bodyType);

    Task<Subscription?> AddSubscriptionAsync(string subscriptionName, decimal price);

    Task<Country?> AddCountryAsync(string country);

    Task<City?> AddCityAsync(string city, int countryId);
        
    Task<DrivetrainType?> AddVehicleDrivetrainTypeAsync(string driveTrainType);

    Task<GearboxType?> AddVehicleGearboxTypeAsync(string gearboxType);

    Task<Make?> AddMakeAsync(string make);

    Task<Model?> AddModelAsync(int makeId, string model);

    Task<FuelType?> AddVehicleFuelTypeAsync(string fuelType);

    Task<Condition?> AddVehicleConditionAsync(string condition, string description);

    Task<MarketVersion?> AddVehicleMarketVersionAsync(string marketVersion);

    Task<Option?> AddVehicleOptionAsync(string option);

    Task<Color?> UpdateVehicleColorAsync(int colorId, string newColor);

    Task<BodyType?> UpdateVehicleBodyTypeAsync(int bodyTypeId, string newBodyType);

    Task<DrivetrainType?> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain);

    Task<GearboxType?> UpdateVehicleGearboxTypeAsync(int gearboxId, string newGearbox);

    Task<AccountLimit?> UpdateAccountLimitAsync(int limitId, int premiumLimit, int regularLimit);

    Task<Subscription?> UpdateSubscriptionAsync(int subscriptionId, decimal price);
        
    Task<Make?> UpdateMakeAsync(int makeId, string newMake);

    Task<Model?> UpdateModelAsync(int modelId, string newModel);

    Task<FuelType?> UpdateFuelTypeAsync(int fuelTypeId, string newFuelType);

    Task<Condition?> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition, string newDescription);

    Task<Country?> UpdateCountryAsync(int countryId, string newCountry);

    Task<City?> UpdateCityAsync(int cityId, string newCity);
        
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

    Task<bool> SendEmailFromStaffAsync(string mail, string subject, string body);

    Task<bool> BanUserAsync(Guid userId);
        
    Task<bool> UnbanUserAsync(Guid userId);
}