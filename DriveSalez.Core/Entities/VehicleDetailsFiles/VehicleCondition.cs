using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleDetailsFiles
{
    public class VehicleCondition
    {
        public int Id { get; set; }

        public string Condition { get; set; }

        public List<VehicleDetails> VehicleDetails { get; set; }
    }
}
