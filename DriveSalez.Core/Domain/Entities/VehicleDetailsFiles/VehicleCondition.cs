using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Domain.Entities.VehicleDetailsFiles
{
    public class VehicleCondition
    {
        public int Id { get; set; }
        
        public string Condition { get; set; }
        
        public string Description { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> VehicleDetails { get; set; }
    }
}
