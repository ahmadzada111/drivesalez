using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Application.DTO.AccountDTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;
using DriveSalez.Domain.Entities.VehicleParts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDetailsRepository _detailsRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
        
    public AdminService(IAdminRepository adminRepository, IHttpContextAccessor contextAccessor, 
        UserManager<ApplicationUser> userManager, IDetailsRepository detailsRepository, 
        IEmailService emailService, IMapper mapper)
    {
        _adminRepository = adminRepository;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _detailsRepository = detailsRepository;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<BodyType?> AddBodyTypeAsync(string bodyType)
    {
        if (string.IsNullOrEmpty(bodyType))
        {
            return null;
        }
            
        var bodyTypes = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();

        if (bodyTypes.Any(x => x.Type == bodyType))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewBodyTypeToDbAsync(bodyType);
        return response;
    }

    public async Task<Color?> AddColorAsync(string color)
    {
        var colors = await _detailsRepository.GetAllColorsFromDbAsync();

        if (colors.Any(x => x.Title == color))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewColorToDbAsync(color);
        return response;
    }

    public async Task<Subscription?> AddSubscriptionAsync(string subscriptionName, decimal price)
    {
        var subscription = await _detailsRepository.GetAllSubscriptionsFromDbAsync();

        if (subscription.Any(x => x.Title == subscriptionName))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewSubscriptionToDbAsync(subscriptionName, price);
        return response;
    }
        
    public async Task<Make?> AddMakeAsync(string make)
    {
        var makes = await _detailsRepository.GetAllMakesFromDbAsync();

        if (makes.Any(x => x.Title == make))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewMakeToDbAsync(make);
        return response;
    }

    public async Task<Model?> AddModelAsync(int makeId, string model)
    {
        var models = await _detailsRepository.GetAllModelsFromDbAsync();

        if (models.Any(x => x.Title == model))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewModelToDbAsync(makeId, model);
        return response;
    }

    public async Task<Condition?> AddVehicleConditionAsync(string condition, string description)
    {
        var vehicleConditions = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();

        if (vehicleConditions.Any(x => x.Title == condition || x.Description == description))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleConditionToDbAsync(condition, description);
        return response;
    }

    public async Task<Option?> AddVehicleOptionAsync(string option)
    {
        var vehicleOptions = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();

        if (vehicleOptions.Any(x => x.Title == option))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleOptionToDbAsync(option);
        return response;
    }

    public async Task<DrivetrainType?> AddVehicleDrivetrainTypeAsync(string driveTrainType)
    {
        var drivetrains = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();

        if (drivetrains.Any(x => x.Type == driveTrainType))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleDrivetrainTypeToDbAsync(driveTrainType);
        return response;
    }

    public async Task<FuelType?> AddVehicleFuelTypeAsync(string fuelType)
    {
        var fuelTypes = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();

        if (fuelTypes.Any(x => x.Type == fuelType))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleFuelTypeToDbAsync(fuelType);
        return response;
    }

    public async Task<GearboxType?> AddVehicleGearboxTypeAsync(string gearboxType)
    {
        var gearboxes = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();

        if (gearboxes.Any(x => x.Type == gearboxType))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleGearboxTypeToDbAsync(gearboxType);
        return response;
    }

    public async Task<Country?> AddCountryAsync(string country)
    {
        var countries = await _detailsRepository.GetAllCountriesFromDbAsync();

        if (countries.Any(x => x.Name == country))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewCountryToDbAsync(country);
        return response;
    }
        
    public async Task<City?> AddCityAsync(string city, int countryId)
    {
        var cities = await _detailsRepository.GetAllCitiesFromDbAsync();

        if (cities.Any(x => x.Name == city))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewCityToDbAsync(city, countryId);
        return response;
    }
        
    public async Task<MarketVersion?> AddVehicleMarketVersionAsync(string marketVersion)
    {
        var marketVersions = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();

        if (marketVersions.Any(x => x.Version == marketVersion))
        {
            return null;
        }
            
        var response = await _adminRepository.SendNewVehicleMarketVersionToDbAsync(marketVersion);
        return response;
    }

    public async Task<Color?> UpdateVehicleColorAsync(int colorId, string newColor)
    {
        var colors = await _detailsRepository.GetAllColorsFromDbAsync();

        if (colors.Any(x => x.Title == newColor))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleColorInDbAsync(colorId, newColor);
        return response;
    }
        
    public async Task<BodyType?> UpdateVehicleBodyTypeAsync(int bodyTypeId, string newBodyType)
    {
        var bodyTypes = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();

        if (bodyTypes.Any(x => x.Type == newBodyType))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleBodyTypeInDbAsync(bodyTypeId, newBodyType);
        return response;
    }
        
    public async Task<Country?> UpdateCountryAsync(int countryId, string newCountry)
    {
        var countries = await _detailsRepository.GetAllCountriesFromDbAsync();

        if (countries.Any(x => x.Name == newCountry))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateCountryInDbAsync(countryId, newCountry);
        return response;
    }
        
    public async Task<City?> UpdateCityAsync(int cityId, string newCity)
    {
        var cities = await _detailsRepository.GetAllCitiesFromDbAsync();

        if (cities.Any(x => x.Name == newCity))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateCityInDbAsync(cityId, newCity);
        return response;
    }
        
    public async Task<AccountLimit?> UpdateAccountLimitAsync(int limitId, int premiumLimit, int regularLimit)
    {
        var response = await _adminRepository.UpdateAccountLimitInDbAsync(limitId, premiumLimit, regularLimit);
        return response;
    }
        
    public async Task<Subscription?> UpdateSubscriptionAsync(int subscriptionId, decimal price)
    {
        var response = await _adminRepository.UpdateSubscriptionInDbAsync(subscriptionId, price);
        return response;
    }
        
    public async Task<DrivetrainType?> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain)
    {
        var drivetrainTypes = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();

        if (drivetrainTypes.Any(x => x.Type == newDrivetrain))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleDrivetrainTypeInDbAsync(drivetrainId, newDrivetrain);
        return response;
    }
        
    public async Task<GearboxType?> UpdateVehicleGearboxTypeAsync(int gearboxId, string newGearbox)
    {
        var gearBoxes = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();

        if (gearBoxes.Any(x => x.Type == newGearbox))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleGearboxTypeInDbAsync(gearboxId, newGearbox);
        return response;
    }

    public async Task<Make?> UpdateMakeAsync(int makeId, string newMake)
    {
        var makes = await _detailsRepository.GetAllMakesFromDbAsync();

        if (makes.Any(x => x.Title == newMake))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateMakeInDbAsync(makeId, newMake);
        return response;
    }
        
    public async Task<Model?> UpdateModelAsync(int modelId, string newModel)
    {
        var models = await _detailsRepository.GetAllModelsFromDbAsync();

        if (models.Any(x => x.Title == newModel))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateModelInDbAsync(modelId, newModel);
        return response;
    }
        
    public async Task<FuelType?> UpdateFuelTypeAsync(int fuelTypeId, string newFuelType)
    {
        var fuelTypes = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();

        if (fuelTypes.Any(x => x.Type == newFuelType))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateFuelTypeInDbAsync(fuelTypeId, newFuelType);
        return response;
    }
        
    public async Task<Condition?> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition, string newDescription)
    {
        var vehicleConditions = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();

        if (vehicleConditions.Any(x => x.Title == newVehicleCondition))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleConditionInDbAsync(vehicleConditionId, newVehicleCondition, newDescription);
        return response;
    }
        
    public async Task<Option?> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption)
    {
        var vehicleOptions = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();

        if (vehicleOptions.Any(x => x.Title == newVehicleOption))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleOptionInDbAsync(vehicleOptionId, newVehicleOption);
        return response;
    }
        
    public async Task<MarketVersion?> UpdateVehicleMarketVersionAsync(int marketVersionId, string newMarketVersion)
    {
        var marketVersions = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();

        if (marketVersions.Any(x => x.Version == newMarketVersion))
        {
            return null;
        }
            
        var response = await _adminRepository.UpdateVehicleMarketVersionInDbAsync(marketVersionId, newMarketVersion);
        return response;
    }

    public async Task<Color?> DeleteVehicleColorAsync(int colorId)
    {
        var response = await _adminRepository.DeleteVehicleColorFromDbAsync(colorId);
        return response;
    }

    public async Task<Country?> DeleteCountryAsync(int countryId)
    {
        var response = await _adminRepository.DeleteCountryFromDbAsync(countryId);
        return response;
    }
        
    public async Task<City?> DeleteCityAsync(int cityId)
    {
        var response = await _adminRepository.DeleteCityFromDbAsync(cityId);
        return response;
    }
        
    public async Task<Subscription?> DeleteSubscriptionAsync(int subscriptionId)
    {
        var response = await _adminRepository.DeleteSubscriptionFromDbAsync(subscriptionId);
        return response;
    }
        
    public async Task<BodyType?> DeleteVehicleBodyTypeAsync(int bodyTypeId)
    {
        var response = await _adminRepository.DeleteVehicleBodyTypeFromDbAsync(bodyTypeId);
        return response;
    }

    public async Task<DrivetrainType?> DeleteVehicleDrivetrainTypeAsync(int drivetrainId)
    {
        var response = await _adminRepository.DeleteVehicleDrivetrainTypeFromDbAsync(drivetrainId);
        return response;
    }

    public async Task<GearboxType?> DeleteVehicleGearboxTypeAsync(int gearboxId)
    {
        var response = await _adminRepository.DeleteVehicleGearboxTypeFromDbAsync(gearboxId);
        return response;
    }

    public async Task<Make?> DeleteMakeAsync(int makeId)
    {
        var response = await _adminRepository.DeleteMakeFromDbAsync(makeId);
        return response;
    }

    public async Task<Model?> DeleteModelAsync(int modelId)
    {
        var response = await _adminRepository.DeleteModelFromDbAsync(modelId);
        return response;
    }

    public async Task<FuelType?> DeleteFuelTypeAsync(int fuelTypeId)
    {
        var response = await _adminRepository.DeleteFuelTypeFromDbAsync(fuelTypeId);
        return response;
    }

    public async Task<Condition?> DeleteVehicleConditionAsync(int vehicleConditionId)
    {
        var response = await _adminRepository.DeleteVehicleConditionFromDbAsync(vehicleConditionId);
        return response;
    }

    public async Task<Option?> DeleteVehicleOptionAsync(int vehicleOptionId)
    {
        var response = await _adminRepository.DeleteVehicleOptionFromDbAsync(vehicleOptionId);
        return response;
    }

    public async Task<MarketVersion?> DeleteVehicleMarketVersionAsync(int marketVersionId)
    {
        var response = await _adminRepository.DeleteVehicleMarketVersionFromDbAsync(marketVersionId);
        return response;
    }

    public async Task<RegisterModeratorResponseDto?> AddModeratorAsync(RegisterModeratorDto request)
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserType.Moderator.ToString());

            return new RegisterModeratorResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        return null;
    }

    public async Task<PaginatedList<GetModeratorDto>> GetAllModeratorsAsync(PagingParameters pagingParameters)
    {
        var moderators = await _userManager.GetUsersInRoleAsync(UserType.Moderator.ToString());

        var result = _mapper.Map<List<GetModeratorDto>>(moderators);
        
        result = result
            .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .ToList();

        var totalCount = result.Count();
        
        return new PaginatedList<GetModeratorDto>(result, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);
    }

    public async Task<GetModeratorDto?> DeleteModeratorAsync(Guid moderatorId)
    {
        var response = await _adminRepository.DeleteModeratorFromDbAsync(moderatorId);
        return _mapper.Map<GetModeratorDto>(response);
    }

    public async Task<PaginatedList<GetUserDto>> GetAllUsers(PagingParameters pagingParameters)
    {
        var response = await _adminRepository.GetAllUsersFromDbAsync(pagingParameters);
        return _mapper.Map<PaginatedList<GetUserDto>>(response);
    }

    public async Task<bool> SendEmailFromStaffAsync(string mail, string subject, string body)
    {
        var result = await _emailService.SendEmailAsync(mail, subject, body);
        return result;
    }

    public async Task<bool> BanUserAsync(Guid userId)
    {
        var result = await _adminRepository.BanUserInDbAsync(userId);
        return result;
    }

    public async Task<bool> UnbanUserAsync(Guid userId)
    {
        var result = await _adminRepository.UnbanUserInDbAsync(userId);
        return result;
    }
}