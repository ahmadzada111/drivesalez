using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;
using DriveSalez.Core.IdentityEntities;

namespace DriveSalez.Core.ServiceContracts
{
    public interface IAdminService
    {
        VehicleColor AddColor(string color);

        VehicleBodyType AddBodyType(string bodyType);

        VehicleDriveTrainType AddVehicleDriveTrainType(string driveTrainType);

        VehicleGearboxType AddVehicleGearboxType(string gearboxType);

        Make AddMake(string make);

        Model AddModel(int makeId, string model);

        VehicleFuelType AddVehicleFuelType(string fuelType);

        VehicleDetailsCondition AddVehicleDetailsCondition(string condition);

        VehicleMarketVersion AddVehicleMarketVersion(string marketVersion);

        VehicleDetailsOptions AddVehicleDetailsOption(string option);

        IEnumerable<VehicleColor> GetAllColors();

        IEnumerable<VehicleBodyType> GetAllVehicleBodyTypes();

        IEnumerable<VehicleDriveTrainType> GetAllVehicleDriveTrains();

        IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypes();

        IEnumerable<Make> GetAllMakes();

        IEnumerable<Model> GetAllModelsByMakeId(int id);

        IEnumerable<VehicleFuelType> GetAllVehicleFuelTypes();

        IEnumerable<VehicleDetailsCondition> GetAllVehicleDetailsConditions();

        IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersions();

        IEnumerable<VehicleDetailsOptions> GetAllVehicleDetailsOptions();

        Task<ApplicationUser> AddModerator(RegisterDto registerDTO);
    }
}
