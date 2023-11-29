using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DriveSalez.Core.Entities
{
    public class AccountPhoneNumber
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
    }
}
