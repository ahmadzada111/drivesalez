using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleParts
{
    public class VehicleFuelType
    {
        public int Id { get; set; }

        public string FuelType { get; set; }

        [JsonIgnore]
        public List<Vehicle> Vehicles { get; set; }        //EF CORE FOREIGN KEY
    }
}
