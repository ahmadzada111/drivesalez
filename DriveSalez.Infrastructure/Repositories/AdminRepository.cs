using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;


namespace DriveSalez.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRepository(ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public VehicleColor SendNewColorToDb(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                return null;
            }

            var response = _dbContext.VehicleColors.Add(new VehicleColor() { Name = color }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleBodyType SendNewBodyTypeToDb(string bodyType)
        {
            if (string.IsNullOrEmpty(bodyType))
            {
                return null;
            }

            var responce = _dbContext.VehicleBodyTypes.Add(new VehicleBodyType() { Name = bodyType }).Entity;
            _dbContext.SaveChanges();
            return responce;
        }

        public VehicleDriveTrainType SendNewVehicleDriveTrainTypeToDb(string driveTrainType)
        {
            if (string.IsNullOrEmpty(driveTrainType))
            {
                return null;
            }

            var response = _dbContext.VehicleDriveTrainTypes.Add(new VehicleDriveTrainType() { Name = driveTrainType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleGearboxType SendNewVehicleGearboxTypeToDb(string gearboxType)
        {
            if (string.IsNullOrEmpty(gearboxType))
            {
                return null;
            }

            var response = _dbContext.VehicleGearboxTypes.Add(new VehicleGearboxType() { Name = gearboxType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public Make SendNewMakeToDb(string make)
        {
            if (string.IsNullOrEmpty(make))
            {
                return null;
            }

            var response = _dbContext.Makes.Add(new Make() { Name = make }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public Model SendNewModelToDb(int makeId, string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return null;
            }

            var response = _dbContext.Models.Add(new Core.Entities.Model() { Name = model, Make = this._dbContext.Makes.Find(makeId) }).Entity;

            if (response != null)
            {
                this._dbContext.SaveChanges();
                return response;
            }

            return null;
        }

        public VehicleFuelType SendNewVehicleFuelTypeToDb(string fuelType)
        {
            if (string.IsNullOrEmpty(fuelType))
            {
                return null;
            }

            var response = _dbContext.VehicleFuelTypes.Add(new VehicleFuelType() { FuelType = fuelType }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleDetailsCondition SendNewVehicleDetailsConditionToDb(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return null;
            }

            var response = _dbContext.VehicleDetailsConditions.Add(new VehicleDetailsCondition() { Condition = condition }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleMarketVersion SendNewVehicleMarketVersionToDb(string marketVersion)
        {
            if (string.IsNullOrEmpty(marketVersion))
            {
                return null;
            }

            var response = _dbContext.VehicleMarketVersions.Add(new VehicleMarketVersion() { Name = marketVersion }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public VehicleDetailsOptions SendNewVehicleDetailsOptionsToDb(string option)
        {
            if (string.IsNullOrEmpty(option))
            {
                return null;
            }

            var response = _dbContext.VehicleDetailsOptions.Add(new VehicleDetailsOptions() { Option = option }).Entity;
            _dbContext.SaveChanges();
            return response;
        }

        public IEnumerable<VehicleColor> GetAllColorsFromDb()
        {
            return _dbContext.VehicleColors.ToList();
        }

        public IEnumerable<VehicleBodyType> GetAllVehicleBodyTypesFromDb()
        {
            return _dbContext.VehicleBodyTypes.ToList();
        }

        public IEnumerable<VehicleDriveTrainType> GetAllVehicleDriveTrainsFromDb()
        {
            return _dbContext.VehicleDriveTrainTypes.ToList();
        }

        public IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypesFromDb()
        {
            return _dbContext.VehicleGearboxTypes.ToList();
        }

        public IEnumerable<Make> GetAllMakesFromDb()
        {
            return _dbContext.Makes.ToList();
        }

        public IEnumerable<Model> GetAllModelsByMakeIdFromDb(int id)
        {
            return _dbContext.Models.Where(e => e.Make.Id == id).ToList();
        }

        public IEnumerable<VehicleFuelType> GetAllVehicleFuelTypesFromDb()
        {
            return _dbContext.VehicleFuelTypes.ToList();
        }

        public IEnumerable<VehicleDetailsCondition> GetAllVehicleDetailsConditionsFromDb()
        {
            return _dbContext.VehicleDetailsConditions.ToList();
        }

        public IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersionsFromDb()
        {
            return _dbContext.VehicleMarketVersions.ToList();
        }

        public IEnumerable<VehicleDetailsOptions> GetAllVehicleDetailsOptionsFromDb()
        {
            return _dbContext.VehicleDetailsOptions.ToList();
        }

        public async Task<ApplicationUser> CreateModeratorInDb(RegisterDto request)
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
