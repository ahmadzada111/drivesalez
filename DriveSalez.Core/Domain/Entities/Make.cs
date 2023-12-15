using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveSalez.Core.Entities
{
    public class Make
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Car Make can't be longer than 30 characters or less than 3.")]
        public string MakeName { get; set; }

        [JsonIgnore]
        public List<Vehicle> Vehicles { get; set; } 
    }
}
