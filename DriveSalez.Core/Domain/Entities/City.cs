using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DriveSalez.Core.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; } 

        public string CityName { get; set; }
        
        public Country? Country { get; set; }    
    }
}
