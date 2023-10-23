using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveSalez.Core.DTO.Enums;

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

        public async Task<VehicleBodyType> AddBodyType(string bodyType)
        {
            var response = await _adminRepository.SendNewBodyTypeToDb(bodyType);
            return response;
        }

        public async Task<VehicleColor> AddColor(string color)
        {
            var response = await _adminRepository.SendNewColorToDb(color);
            return response;
        }

        public async Task<Make> AddMake(string make)
        {
            var response = await _adminRepository.SendNewMakeToDb(make);
            return response;
        }

        public async Task<Model> AddModel(int makeId, string model)
        {
            var response = await _adminRepository.SendNewModelToDb(makeId, model);
            return response;
        }

        public async Task<VehicleCondition> AddVehicleCondition(string condition)
        {
            var response = await _adminRepository.SendNewVehicleDetailsConditionToDb(condition);
            return response;
        }

        public async Task<VehicleOption> AddVehicleOption(string option)
        {
            var response = await _adminRepository.SendNewVehicleDetailsOptionsToDb(option);
            return response;
        }

        public async Task<VehicleDrivetrainType> AddVehicleDrivetrainType(string driveTrainType)
        {
            var response = await _adminRepository.SendNewVehicleDrivetrainTypeToDb(driveTrainType);
            return response;
        }

        public async Task<VehicleFuelType> AddVehicleFuelType(string fuelType)
        {
            var response = await _adminRepository.SendNewVehicleFuelTypeToDb(fuelType);
            return response;
        }

        public async Task<VehicleGearboxType> AddVehicleGearboxType(string gearboxType)
        {
            var response = await _adminRepository.SendNewVehicleGearboxTypeToDb(gearboxType);
            return response;
        }

        public async Task<VehicleMarketVersion> AddVehicleMarketVersion(string marketVersion)
        {
            var response = await _adminRepository.SendNewVehicleMarketVersionToDb(marketVersion);
            return response;
        }

        public async Task<VehicleColor> UpdateVehicleColor(int colorId, string newColor)
        {
            var response = await _adminRepository.UpdateVehicleColorInDb(colorId, newColor);
            return response;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyType(int bodyTypeId, string newBodyType)
        {
            var response = await _adminRepository.UpdateVehicleBodyTypeInDb(bodyTypeId, newBodyType);
            return response;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainType(int drivetrainId, string newDrivetrain)
        {
            var response = await _adminRepository.UpdateVehicleDrivetrainTypeInDb(drivetrainId, newDrivetrain);
            return response;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxType(int gearboxId, string newGearbox)
        {
            var response = await _adminRepository.UpdateVehicleGearboxTypeInDb(gearboxId, newGearbox);
            return response;
        }

        public async Task<Make> UpdateMake(int makeId, string newMake)
        {
            var response = await _adminRepository.UpdateMakeInDb(makeId, newMake);
            return response;
        }
        
        public async Task<Model> UpdateModel(int modelId, string newModel)
        {
            var response = await _adminRepository.UpdateModelInDb(modelId, newModel);
            return response;
        }
        
        public async Task<VehicleFuelType> UpdateFuelType(int fuelTypeId, string newFuelType)
        {
            var response = await _adminRepository.UpdateFuelTypeInDb(fuelTypeId, newFuelType);
            return response;
        }
        
        public async Task<VehicleCondition> UpdateVehicleCondition(int vehicleConditionId, string newVehicleCondition)
        {
            var response = await _adminRepository.UpdateVehicleConditionInDb(vehicleConditionId, newVehicleCondition);
            return response;
        }
        
        public async Task<VehicleOption> UpdateVehicleOption(int vehicleOptionId, string newVehicleOption)
        {
            var response = await _adminRepository.UpdateVehicleOptionInDb(vehicleOptionId, newVehicleOption);
            return response;
        }
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersion(int marketVersionId, string newMarketVersion)
        {
            var response = await _adminRepository.UpdateVehicleMarketVersionInDb(marketVersionId, newMarketVersion);
            return response;
        }

        public async Task<VehicleColor> DeleteVehicleColor(int colorId)
        {
            var response = await _adminRepository.DeleteVehicleColorFromDb(colorId);
            return response;
        }

        public async Task<VehicleBodyType> DeleteVehicleBodyType(int bodyTypeId)
        {
            var response = await _adminRepository.DeleteVehicleBodyTypeFromDb(bodyTypeId);
            return response;
        }

        public async Task<VehicleDrivetrainType> DeleteVehicleDrivetrainType(int drivetrainId)
        {
            var response = await _adminRepository.DeleteVehicleDrivetrainTypeFromDb(drivetrainId);
            return response;
        }

        public async Task<VehicleGearboxType> DeleteVehicleGearboxType(int gearboxId)
        {
            var response = await _adminRepository.DeleteVehicleGearboxTypeFromDb(gearboxId);
            return response;
        }

        public async Task<Make> DeleteMake(int makeId)
        {
            var response = await _adminRepository.DeleteMakeFromDb(makeId);
            return response;
        }

        public async Task<Model> DeleteModel(int modelId)
        {
            var response = await _adminRepository.DeleteModelFromDb(modelId);
            return response;
        }

        public async Task<VehicleFuelType> DeleteFuelType(int fuelTypeId)
        {
            var response = await _adminRepository.DeleteFuelTypeFromDb(fuelTypeId);
            return response;
        }

        public async Task<VehicleCondition> DeleteVehicleCondition(int vehicleConditionId)
        {
            var response = await _adminRepository.DeleteVehicleConditionFromDb(vehicleConditionId);
            return response;
        }

        public async Task<VehicleOption> DeleteVehicleOption(int vehicleOptionId)
        {
            var response = await _adminRepository.DeleteVehicleOptionFromDb(vehicleOptionId);
            return response;
        }

        public async Task<VehicleMarketVersion> DeleteVehicleMarketVersion(int marketVersionId)
        {
            var response = await _adminRepository.DeleteVehicleMarketVersionFromDb(marketVersionId);
            return response;
        }

        public async Task<ApplicationUser> AddModerator(RegisterDto request)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = request.Email,
                PhoneNumber = request.Phone,
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

                return user;
            }

            return null;
        }
    }
}
