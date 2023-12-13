using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
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

        public AdminService(IAdminRepository adminRepository, IHttpContextAccessor contextAccessor, 
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _adminRepository = adminRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<VehicleBodyType> AddBodyTypeAsync(string bodyType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
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
            
            var response = await _adminRepository.SendNewVehicleDetailsConditionToDbAsync(condition, description);
            return response;
        }

        public async Task<VehicleOption> AddVehicleOptionAsync(string option)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.SendNewVehicleDetailsOptionsToDbAsync(option);
            return response;
        }

        public async Task<VehicleDrivetrainType> AddVehicleDrivetrainTypeAsync(string driveTrainType)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
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
            
            var response = await _adminRepository.SendNewVehicleGearboxTypeToDbAsync(gearboxType);
            return response;
        }

        public async Task<VehicleMarketVersion> AddVehicleMarketVersionAsync(string marketVersion)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
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
            
            var response = await _adminRepository.UpdateVehicleBodyTypeInDbAsync(bodyTypeId, newBodyType);
            return response;
        }
        
        public async Task<AccountLimit> UpdateAccountLimitAsync(int limitId, int limit)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.UpdateAccountLimitInDbAsync(limitId, limit);
            return response;
        }
        
        public async Task<Currency> UpdateCurrencyAsync(int currencyId, string currencyName)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.UpdateCurrencyInDbAsync(currencyId, currencyName);
            return response;
        }
        
        public async Task<Subscription> UpdateSubscriptionAsync(int subscriptionId,string subscriptionName, decimal price, int currencyId)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.UpdateSubscriptionInDbAsync(subscriptionId, subscriptionName, price, currencyId);
            return response;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
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
            
            var response = await _adminRepository.UpdateFuelTypeInDbAsync(fuelTypeId, newFuelType);
            return response;
        }
        
        public async Task<VehicleCondition> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
            }
            
            var response = await _adminRepository.UpdateVehicleConditionInDbAsync(vehicleConditionId, newVehicleCondition);
            return response;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new UserNotAuthorizedException("User is not authorized!");
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
    }
}
