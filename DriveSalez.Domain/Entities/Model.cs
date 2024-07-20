using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class Model
{
    [Key]
    public int Id { get; set; }
        
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Car Model can't be longer than 30 characters or less than 3.")]
    public string ModelName { get; set; }

    public Make Make { get; set; }

    [JsonIgnore]
    public List<Vehicle> Vehicles { get; set; }
}