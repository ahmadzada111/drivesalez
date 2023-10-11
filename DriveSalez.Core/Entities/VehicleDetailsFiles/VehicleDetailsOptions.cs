using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities.VehicleDetailsFiles
{
    public class VehicleDetailsOptions
    {
        public int Id { get; set; }

        public string Option { get; set; }

        public List<VehicleDetails> Details { get; set; }
    }
}
