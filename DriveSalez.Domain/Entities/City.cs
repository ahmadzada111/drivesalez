using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.Domain.Entities;

namespace DriveSalez.Domain.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; } 

        public string CityName { get; set; }
        
        public Country? Country { get; set; }    
    }
}
