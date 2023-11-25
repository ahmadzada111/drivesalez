using System.ComponentModel.DataAnnotations;
using DriveSalez.Core.Entities.VehicleDetailsFiles;
using DriveSalez.Core.Entities.VehicleParts;

namespace DriveSalez.Core.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Year")]
        public ManufactureYear? Year { get; set; }   // 2009

        public Make? Make { get; set; }

        public Model? Model { get; set; }

        [Required(ErrorMessage = "Please enter Fuel Type")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Fuel Type can't be longer than 15 characters or less than 3.")]
        public VehicleFuelType? FuelType { get; set; }

        [Required(ErrorMessage = "Please enter state of your car")]
        public bool? IsBrandNew { get; set; }       //NEW Or USED

        public VehicleDetails VehicleDetails { get; set; }
    }
}
