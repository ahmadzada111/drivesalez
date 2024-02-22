using System.Text.Json.Serialization;

namespace DriveSalez.Core.Domain.Entities.VehicleParts
{
    public class VehicleFuelType
    {
        public int Id { get; set; }

        public string FuelType { get; set; }

        [JsonIgnore]
        public List<Vehicle> Vehicles { get; set; }        //EF CORE FOREIGN KEY
    }
}
