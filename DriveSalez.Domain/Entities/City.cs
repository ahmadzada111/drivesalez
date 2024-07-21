using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class City
{
    [Key]
    public int Id { get; set; } 

    public string CityName { get; set; }
        
    [JsonIgnore]
    public Country? Country { get; set; }    
}