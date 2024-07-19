using System.Text.Json.Serialization;
using DriveSalez.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Core.Domain.Entities.VehicleParts
{

    public class VehicleGearboxType
    {
        public int Id { get; set; }

        public string GearboxType { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> VehicleDetails { get; set; }        //EF CORE FOREIGN KEY
    }
}
