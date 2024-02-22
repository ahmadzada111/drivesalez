using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.Domain.Entities.VehicleParts;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.Domain.Entities.VehicleDetailsFiles
{
    public class VehicleDetails
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Car Make can't be longer than 30 characters or less than 3.")]
        public VehicleBodyType BodyType { get; set; }    // Sedan,Pick-up, Truck, Roadster     

        public VehicleColor Color { get; set; }       // Red

        public int HorsePower { get; set; }     // 150 hp

        public VehicleGearboxType GearboxType { get; set; }     // Manual

        public VehicleDrivetrainType DrivetrainType { get; set; }  // FWD

        public List<VehicleCondition> Conditions { get; set; }  //No Damage, No Color

        public VehicleMarketVersion MarketVersion { get; set; }      // US

        public int? OwnerQuantity { get; set; }

        public int? SeatCount { get; set; }

        public string? VinCode { get; set; }

        public List<VehicleOption> Options { get; set; }      // ABS, REAR CAMERA, FRONT RADAR

        public int? EngineVolume { get; set; }     //6200 CC

        public int MileAge { get; set; }    //149000 km

        public DistanceUnit MileageType { get; set; }
    }
}
