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
using DriveSalez.Core.DTO.Pagination;

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
            var response = await _adminRepository.SendNewBodyTypeToDbAsync(bodyType);
            return response;
        }

        public async Task<VehicleColor> AddColorAsync(string color)
        {
            var response = await _adminRepository.SendNewColorToDbAsync(color);
            return response;
        }

        public async Task<Make> AddMakeAsync(string make)
        {
            var response = await _adminRepository.SendNewMakeToDbAsync(make);
            return response;
        }

        public async Task<Model> AddModelAsync(int makeId, string model)
        {
            var response = await _adminRepository.SendNewModelToDbAsync(makeId, model);
            return response;
        }

        public async Task<VehicleCondition> AddVehicleConditionAsync(string condition)
        {
            var response = await _adminRepository.SendNewVehicleDetailsConditionToDbAsync(condition);
            return response;
        }

        public async Task<VehicleOption> AddVehicleOptionAsync(string option)
        {
            var response = await _adminRepository.SendNewVehicleDetailsOptionsToDbAsync(option);
            return response;
        }

        public async Task<VehicleDrivetrainType> AddVehicleDrivetrainTypeAsync(string driveTrainType)
        {
            var response = await _adminRepository.SendNewVehicleDrivetrainTypeToDbAsync(driveTrainType);
            return response;
        }

        public async Task<VehicleFuelType> AddVehicleFuelTypeAsync(string fuelType)
        {
            var response = await _adminRepository.SendNewVehicleFuelTypeToDbAsync(fuelType);
            return response;
        }

        public async Task<VehicleGearboxType> AddVehicleGearboxTypeAsync(string gearboxType)
        {
            var response = await _adminRepository.SendNewVehicleGearboxTypeToDbAsync(gearboxType);
            return response;
        }

        public async Task<VehicleMarketVersion> AddVehicleMarketVersionAsync(string marketVersion)
        {
            var response = await _adminRepository.SendNewVehicleMarketVersionToDbAsync(marketVersion);
            return response;
        }

        public async Task<VehicleColor> UpdateVehicleColorAsync(int colorId, string newColor)
        {
            var response = await _adminRepository.UpdateVehicleColorInDbAsync(colorId, newColor);
            return response;
        }
        
        public async Task<VehicleBodyType> UpdateVehicleBodyTypeAsync(int bodyTypeId, string newBodyType)
        {
            var response = await _adminRepository.UpdateVehicleBodyTypeInDbAsync(bodyTypeId, newBodyType);
            return response;
        }
        
        public async Task<VehicleDrivetrainType> UpdateVehicleDrivetrainTypeAsync(int drivetrainId, string newDrivetrain)
        {
            var response = await _adminRepository.UpdateVehicleDrivetrainTypeInDbAsync(drivetrainId, newDrivetrain);
            return response;
        }
        
        public async Task<VehicleGearboxType> UpdateVehicleGearboxTypeAsync(int gearboxId, string newGearbox)
        {
            var response = await _adminRepository.UpdateVehicleGearboxTypeInDbAsync(gearboxId, newGearbox);
            return response;
        }

        public async Task<Make> UpdateMakeAsync(int makeId, string newMake)
        {
            var response = await _adminRepository.UpdateMakeInDbAsync(makeId, newMake);
            return response;
        }
        
        public async Task<Model> UpdateModelAsync(int modelId, string newModel)
        {
            var response = await _adminRepository.UpdateModelInDbAsync(modelId, newModel);
            return response;
        }
        
        public async Task<VehicleFuelType> UpdateFuelTypeAsync(int fuelTypeId, string newFuelType)
        {
            var response = await _adminRepository.UpdateFuelTypeInDbAsync(fuelTypeId, newFuelType);
            return response;
        }
        
        public async Task<VehicleCondition> UpdateVehicleConditionAsync(int vehicleConditionId, string newVehicleCondition)
        {
            var response = await _adminRepository.UpdateVehicleConditionInDbAsync(vehicleConditionId, newVehicleCondition);
            return response;
        }
        
        public async Task<VehicleOption> UpdateVehicleOptionAsync(int vehicleOptionId, string newVehicleOption)
        {
            var response = await _adminRepository.UpdateVehicleOptionInDbAsync(vehicleOptionId, newVehicleOption);
            return response;
        }
        
        public async Task<VehicleMarketVersion> UpdateVehicleMarketVersionAsync(int marketVersionId, string newMarketVersion)
        {
            var response = await _adminRepository.UpdateVehicleMarketVersionInDbAsync(marketVersionId, newMarketVersion);
            return response;
        }

        public async Task<VehicleColor> DeleteVehicleColorAsync(int colorId)
        {
            var response = await _adminRepository.DeleteVehicleColorFromDbAsync(colorId);
            return response;
        }

        public async Task<VehicleBodyType> DeleteVehicleBodyTypeAsync(int bodyTypeId)
        {
            var response = await _adminRepository.DeleteVehicleBodyTypeFromDbAsync(bodyTypeId);
            return response;
        }

        public async Task<VehicleDrivetrainType> DeleteVehicleDrivetrainTypeAsync(int drivetrainId)
        {
            var response = await _adminRepository.DeleteVehicleDrivetrainTypeFromDbAsync(drivetrainId);
            return response;
        }

        public async Task<VehicleGearboxType> DeleteVehicleGearboxTypeAsync(int gearboxId)
        {
            var response = await _adminRepository.DeleteVehicleGearboxTypeFromDbAsync(gearboxId);
            return response;
        }

        public async Task<Make> DeleteMakeAsync(int makeId)
        {
            var response = await _adminRepository.DeleteMakeFromDbAsync(makeId);
            return response;
        }

        public async Task<Model> DeleteModelAsync(int modelId)
        {
            var response = await _adminRepository.DeleteModelFromDbAsync(modelId);
            return response;
        }

        public async Task<VehicleFuelType> DeleteFuelTypeAsync(int fuelTypeId)
        {
            var response = await _adminRepository.DeleteFuelTypeFromDbAsync(fuelTypeId);
            return response;
        }

        public async Task<VehicleCondition> DeleteVehicleConditionAsync(int vehicleConditionId)
        {
            var response = await _adminRepository.DeleteVehicleConditionFromDbAsync(vehicleConditionId);
            return response;
        }

        public async Task<VehicleOption> DeleteVehicleOptionAsync(int vehicleOptionId)
        {
            var response = await _adminRepository.DeleteVehicleOptionFromDbAsync(vehicleOptionId);
            return response;
        }

        public async Task<VehicleMarketVersion> DeleteVehicleMarketVersionAsync(int marketVersionId)
        {
            var response = await _adminRepository.DeleteVehicleMarketVersionFromDbAsync(marketVersionId);
            return response;
        }

        public async Task<RegisterModeratorResponseDto> AddModeratorAsync(RegisterModeratorDto request)
        {
            ApplicationUser user = new ApplicationUser()
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
