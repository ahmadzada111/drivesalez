using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleDetailsFiles
{
    public class VehicleCondition
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> VehicleDetails { get; set; }
    }
}
