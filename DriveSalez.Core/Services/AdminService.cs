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

        public VehicleBodyType AddBodyType(string bodyType)
        {
            var response = _adminRepository.SendNewBodyTypeToDb(bodyType);
            return response;
        }

        public VehicleColor AddColor(string color)
        {
            var response = _adminRepository.SendNewColorToDb(color);
            return response;
        }

        public Make AddMake(string make)
        {
            var response = _adminRepository.SendNewMakeToDb(make);
            return response;
        }

        public Model AddModel(int makeId, string model)
        {
            var response = _adminRepository.SendNewModelToDb(makeId, model);
            return response;
        }

        public VehicleDetailsCondition AddVehicleDetailsCondition(string condition)
        {
            var response = _adminRepository.SendNewVehicleDetailsConditionToDb(condition);
            return response;
        }

        public VehicleDetailsOptions AddVehicleDetailsOption(string option)
        {
            var response = _adminRepository.SendNewVehicleDetailsOptionsToDb(option);
            return response;
        }

        public VehicleDriveTrainType AddVehicleDriveTrainType(string driveTrainType)
        {
            var response = _adminRepository.SendNewVehicleDriveTrainTypeToDb(driveTrainType);
            return response;
        }

        public VehicleFuelType AddVehicleFuelType(string fuelType)
        {
            var response = _adminRepository.SendNewVehicleFuelTypeToDb(fuelType);
            return response;
        }

        public VehicleGearboxType AddVehicleGearboxType(string gearboxType)
        {
            var response = _adminRepository.SendNewVehicleGearboxTypeToDb(gearboxType);
            return response;
        }

        public VehicleMarketVersion AddVehicleMarketVersion(string marketVersion)
        {
            var response = _adminRepository.SendNewVehicleMarketVersionToDb(marketVersion);
            return response;
        }

        public IEnumerable<VehicleColor> GetAllColors()
        {
            var response = _adminRepository.GetAllColorsFromDb();
            return response;
        }

        public IEnumerable<VehicleBodyType> GetAllVehicleBodyTypes()
        {
            var response = _adminRepository.GetAllVehicleBodyTypesFromDb();
            return response;
        }

        public IEnumerable<VehicleDriveTrainType> GetAllVehicleDriveTrains()
        {
            var response = _adminRepository.GetAllVehicleDriveTrainsFromDb();
            return response;
        }

        public IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypes()
        {
            var response = _adminRepository.GetAllVehicleGearboxTypesFromDb();
            return response;
        }

        public IEnumerable<Make> GetAllMakes()
        {
            var response = _adminRepository.GetAllMakesFromDb();
            return response;
        }
        
        public IEnumerable<VehicleFuelType> GetAllVehicleFuelTypes()
        {
            var response = _adminRepository.GetAllVehicleFuelTypesFromDb();
            return response;
        }

        public IEnumerable<VehicleDetailsCondition> GetAllVehicleDetailsConditions()
        {
            var response = _adminRepository.GetAllVehicleDetailsConditionsFromDb();
            return response;
        }

        public IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersions()
        {
            var response = _adminRepository.GetAllVehicleMarketVersionsFromDb();
            return response;
        }

        public IEnumerable<VehicleDetailsOptions> GetAllVehicleDetailsOptions()
        {
            var response = _adminRepository.GetAllVehicleDetailsOptionsFromDb();
            return response;
        }

        public IEnumerable<Model> GetAllModelsByMakeId(int id)
        {
            var response = _adminRepository.GetAllModelsByMakeIdFromDb(id);
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
