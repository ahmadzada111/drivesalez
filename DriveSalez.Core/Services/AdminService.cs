using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Exceptions;

namespace DriveSalez.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IDetailsRepository _detailsRepository;
        
        public AdminService(IAdminRepository adminRepository, IHttpContextAccessor contextAccessor, 
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IDetailsRepository detailsRepository)
        {
            _adminRepository = adminRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
            _detailsRepository = detailsRepository;
        }

        public async Task<VehicleBodyType> AddBodyTypeAsync(string bodyType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var bodyTypes = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();

            if (bodyTypes.Any(x => x.BodyType == bodyType))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewBodyTypeToDbAsync(bodyType);
            return response;
        }

        public async Task<VehicleColor> AddColorAsync(string color)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var colors = await _detailsRepository.GetAllColorsFromDbAsync();

            if (colors.Any(x => x.Color == color))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewColorToDbAsync(color);
            return response;
        }

        public async Task<Subscription> AddSubscriptionAsync(string subscriptionName, decimal price, int currencyId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var subscription = await _detailsRepository.GetAllSubscriptionsFromDbAsync();

            if (subscription.Any(x => x.SubscriptionName == subscriptionName))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewSubscriptionToDbAsync(subscriptionName, price, currencyId);
            return response;
        }
        
        public async Task<Currency> AddCurrencyAsync(string currencyName)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var currencies = await _detailsRepository.GetAllCurrenciesFromDbAsync();

            if (currencies.Any(x => x.CurrencyName == currencyName))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewCurrencyToDbAsync(currencyName);
            return response;
        }
        
        public async Task<Make> AddMakeAsync(string make)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var makes = await _detailsRepository.GetAllMakesFromDbAsync();

            if (makes.Any(x => x.MakeName == make))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewMakeToDbAsync(make);
            return response;
        }

        public async Task<Model> AddModelAsync(int makeId, string model)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var models = await _detailsRepository.GetAllModelsFromDbAsync();

            if (models.Any(x => x.ModelName == model))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewModelToDbAsync(makeId, model);
            return response;
        }

        public async Task<VehicleCondition> AddVehicleConditionAsync(string condition, string description)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var vehicleConditions = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();

            if (vehicleConditions.Any(x => x.Condition == condition || x.Description == description))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleConditionToDbAsync(condition, description);
            return response;
        }

        public async Task<VehicleOption> AddVehicleOptionAsync(string option)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var vehicleOptions = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();

            if (vehicleOptions.Any(x => x.Option == option))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleOptionToDbAsync(option);
            return response;
        }

        public async Task<VehicleDrivetrainType> AddVehicleDrivetrainTypeAsync(string driveTrainType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var drivetrains = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();

            if (drivetrains.Any(x => x.DrivetrainType == driveTrainType))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleDrivetrainTypeToDbAsync(driveTrainType);
            return response;
        }

        public async Task<VehicleFuelType> AddVehicleFuelTypeAsync(string fuelType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var fuelTypes = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();

            if (fuelTypes.Any(x => x.FuelType == fuelType))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleFuelTypeToDbAsync(fuelType);
            return response;
        }

        public async Task<VehicleGearboxType> AddVehicleGearboxTypeAsync(string gearboxType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var gearboxes = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();

            if (gearboxes.Any(x => x.GearboxType == gearboxType))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleGearboxTypeToDbAsync(gearboxType);
            return response;
        }

        public async Task<Country> AddCountryAsync(string country)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var countries = await _detailsRepository.GetAllCountriesFromDbAsync();

            if (countries.Any(x => x.CountryName == country))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewCountryToDbAsync(country);
            return response;
        }
        
        public async Task<City> AddCityAsync(string city, int countryId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var cities = await _detailsRepository.GetAllCitiesFromDbAsync();

            if (cities.Any(x => x.CityName == city))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewCityToDbAsync(city, countryId);
            return response;
        }
        
        public async Task<VehicleMarketVersion> AddVehicleMarketVersionAsync(string marketVersion)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }

            var marketVersions = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();

            if (marketVersions.Any(x => x.MarketVersion == marketVersion))
            {
                return null;
            }
            
            var response = await _adminRepository.SendNewVehicleMarketVersionToDbAsync(marketVersion);
            return response;
        }

        public async Task<VehicleColor> UpdateVehicleColorAsync(int colorId, string newColor)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var colors = await _detailsRepository.GetAllColorsFromDbAsync();

            if (colors.Any(x => x.Color == newColor))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleColorInDbAsync(colorId, newColor);
            return response;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeAsync(int bodyTypeId, string newBodyType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var bodyTypes = await _detailsRepository.GetAllVehicleBodyTypesFromDbAsync();

            if (bodyTypes.Any(x => x.BodyType == newBodyType))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleBodyTypeInDbAsync(bodyTypeId, newBodyType);
            return response;
        }
        
        public async Task<Country> UpdateCountryAsync(int countryId, string newCountry)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var countries = await _detailsRepository.GetAllCountriesFromDbAsync();

            if (countries.Any(x => x.CountryName == newCountry))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateCountryInDbAsync(countryId, newCountry);
            return response;
        }
        
        public async Task<City> UpdateCityAsync(int cityId, string newCity)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var cities = await _detailsRepository.GetAllCitiesFromDbAsync();

            if (cities.Any(x => x.CityName == newCity))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateCityInDbAsync(cityId, newCity);
            return response;
        }
        
        //CHECK!
        public async Task<AccountLimit> UpdateAccountLimitAsync(int limitId, int premiumLimit, int regularLimit)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.UpdateAccountLimitInDbAsync(limitId, premiumLimit, regularLimit);
            return response;
        }
        
        public async Task<Currency> UpdateCurrencyAsync(int currencyId, string currencyName)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var currencies = await _detailsRepository.GetAllCurrenciesFromDbAsync();

            if (currencies.Any(x => x.CurrencyName == currencyName))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateCurrencyInDbAsync(currencyId, currencyName);
            return response;
        }
        
        public async Task<Subscription> UpdateSubscriptionAsync(int subscriptionId, decimal price, int currencyId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var subscriptions = await _detailsRepository.GetAllSubscriptionsFromDbAsync();
            
            var response = await _adminRepository.UpdateSubscriptionInDbAsync(subscriptionId, price, currencyId);
            return response;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var drivetrainTypes = await _detailsRepository.GetAllVehicleDrivetrainsFromDbAsync();

            if (drivetrainTypes.Any(x => x.DrivetrainType == newDrivetrain))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleDrivetrainTypeInDbAsync(drivetrainId, newDrivetrain);
            return response;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeAsync(int gearboxId, string newGearbox)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var gearBoxes = await _detailsRepository.GetAllVehicleGearboxTypesFromDbAsync();

            if (gearBoxes.Any(x => x.GearboxType == newGearbox))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleGearboxTypeInDbAsync(gearboxId, newGearbox);
            return response;
        }

        public async Task<Make> UpdateMakeAsync(int makeId, string newMake)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var makes = await _detailsRepository.GetAllMakesFromDbAsync();

            if (makes.Any(x => x.MakeName == newMake))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateMakeInDbAsync(makeId, newMake);
            return response;
        }
        
        public async Task<Model> UpdateModelAsync(int modelId, string newModel)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var models = await _detailsRepository.GetAllModelsFromDbAsync();

            if (models.Any(x => x.ModelName == newModel))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateModelInDbAsync(modelId, newModel);
            return response;
        }
        
        public async Task<VehicleFuelType> UpdateFuelTypeAsync(int fuelTypeId, string newFuelType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var fuelTypes = await _detailsRepository.GetAllVehicleFuelTypesFromDbAsync();

            if (fuelTypes.Any(x => x.FuelType == newFuelType))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateFuelTypeInDbAsync(fuelTypeId, newFuelType);
            return response;
        }
        
        public async Task<VehicleCondition> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition, string newDescription)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var vehicleConditions = await _detailsRepository.GetAllVehicleDetailsConditionsFromDbAsync();

            if (vehicleConditions.Any(x => x.Condition == newVehicleCondition))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleConditionInDbAsync(vehicleConditionId, newVehicleCondition, newDescription);
            return response;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var vehicleOptions = await _detailsRepository.GetAllVehicleDetailsOptionsFromDbAsync();

            if (vehicleOptions.Any(x => x.Option == newVehicleOption))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleOptionInDbAsync(vehicleOptionId, newVehicleOption);
            return response;
        }
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionAsync(int marketVersionId, string newMarketVersion)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var marketVersions = await _detailsRepository.GetAllVehicleMarketVersionsFromDbAsync();

            if (marketVersions.Any(x => x.MarketVersion == newMarketVersion))
            {
                return null;
            }
            
            var response = await _adminRepository.UpdateVehicleMarketVersionInDbAsync(marketVersionId, newMarketVersion);
            return response;
        }

        public async Task<VehicleColor> DeleteVehicleColorAsync(int colorId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleColorFromDbAsync(colorId);
            return response;
        }

        public async Task<Country> DeleteCountryAsync(int countryId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteCountryFromDbAsync(countryId);
            return response;
        }
        
        public async Task<City> DeleteCityAsync(int cityId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteCityFromDbAsync(cityId);
            return response;
        }
        
        public async Task<Currency> DeleteCurrencyAsync(int currencyId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteCurrencyFromDbAsync(currencyId);
            return response;
        }
        
        public async Task<Subscription> DeleteSubscriptionAsync(int subscriptionId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteSubscriptionFromDbAsync(subscriptionId);
            return response;
        }
        
        public async Task<VehicleBodyType> DeleteVehicleBodyTypeAsync(int bodyTypeId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleBodyTypeFromDbAsync(bodyTypeId);
            return response;
        }

        public async Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeAsync(int drivetrainId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleDrivetrainTypeFromDbAsync(drivetrainId);
            return response;
        }

        public async Task<VehicleGearboxType> DeleteVehicleGearboxTypeAsync(int gearboxId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleGearboxTypeFromDbAsync(gearboxId);
            return response;
        }

        public async Task<Make> DeleteMakeAsync(int makeId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteMakeFromDbAsync(makeId);
            return response;
        }

        public async Task<Model> DeleteModelAsync(int modelId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteModelFromDbAsync(modelId);
            return response;
        }

        public async Task<VehicleFuelType> DeleteFuelTypeAsync(int fuelTypeId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteFuelTypeFromDbAsync(fuelTypeId);
            return response;
        }

        public async Task<VehicleCondition> DeleteVehicleConditionAsync(int vehicleConditionId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleConditionFromDbAsync(vehicleConditionId);
            return response;
        }

        public async Task<VehicleOption> DeleteVehicleOptionAsync(int vehicleOptionId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleOptionFromDbAsync(vehicleOptionId);
            return response;
        }

        public async Task<VehicleMarketVersion> DeleteVehicleMarketVersionAsync(int marketVersionId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteVehicleMarketVersionFromDbAsync(marketVersionId);
            return response;
        }

        public async Task<RegisterModeratorResponseDto> AddModeratorAsync(RegisterModeratorDto request)
        {
            var admin = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (admin == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            DefaultAccount user = new DefaultAccount()
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(UserType.Moderator.ToString()) == null)
                {
                    ApplicationRole applicationRole = new ApplicationRole()
                    {
                        Name = UserType.Moderator.ToString()
                    };

                    await _roleManager.CreateAsync(applicationRole);
                    await _userManager.AddToRoleAsync(user, UserType.Moderator.ToString());
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, UserType.Moderator.ToString());
                }

                return new RegisterModeratorResponseDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }

            return null;
        }

        public async Task<List<GetModeratorDto>> GetAllModeratorsAsync()
        {
            var moderators = await _userManager.GetUsersInRoleAsync(UserType.Moderator.ToString());
            List<GetModeratorDto> result = new List<GetModeratorDto>();
            
            foreach (var moderator in moderators)
            {
                result.Add(new GetModeratorDto()
                {
                    Id = moderator.Id,
                    Name = moderator.FirstName,
                    Surname = moderator.LastName,
                    Email = moderator.UserName
                });
            }

            return result;
        }

        public async Task<GetModeratorDto> DeleteModeratorAsync(Guid moderatorId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.DeleteModeratorFromDbAsync(moderatorId);
            return response;
        }
    }
}
