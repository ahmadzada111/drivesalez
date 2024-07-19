using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Domain.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string CountryName { get; set; }
    }
}
