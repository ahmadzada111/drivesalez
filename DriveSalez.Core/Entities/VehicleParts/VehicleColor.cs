using DriveSalez.Core.Entities.VehicleDetailsFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleParts
{
    public class VehicleColor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> VehicleDetails { get; set; }        //EF CORE FOREIGN KEY
    }
}
