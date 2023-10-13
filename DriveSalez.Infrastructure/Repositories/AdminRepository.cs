using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Infrastructure.DbContext;

namespace DriveSalez.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
