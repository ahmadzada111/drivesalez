using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities
{
    public class CarDealerPhoneNumber
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public BusinessAccount Dealer { get; set; }       //EF CORE FOREIGN KEY
    }
}
