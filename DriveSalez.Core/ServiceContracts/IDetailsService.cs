using DriveSalez.Core.Entities;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.ServiceContracts;

public interface IDetailsService
{
    IEnumerable<VehicleColor> GetAllColors();

    IEnumerable<VehicleBodyType> GetAllVehicleBodyTypes();

    IEnumerable<VehicleDrivetrainType> GetAllVehicleDrivetrains();

    IEnumerable<VehicleGearboxType> GetAllVehicleGearboxTypes();

    IEnumerable<Make> GetAllMakes();

    IEnumerable<Model> GetAllModelsByMakeId(int id);

    IEnumerable<VehicleFuelType> GetAllVehicleFuelTypes();

    IEnumerable<VehicleCondition> GetAllVehicleDetailsConditions();

    IEnumerable<VehicleMarketVersion> GetAllVehicleMarketVersions();

    IEnumerable<VehicleOption> GetAllVehicleDetailsOptions();
}