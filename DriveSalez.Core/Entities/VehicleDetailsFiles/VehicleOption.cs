using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleDetailsFiles
{
    public class VehicleOption
    {
        public int Id { get; set; }

        public string Option { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> Details { get; set; }
    }
}
