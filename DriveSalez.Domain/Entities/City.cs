using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Domain.Entities;

public class City
{
    [Key]
    public int Id { get; set; } 

    public string CityName { get; set; }
        
    public Country? Country { get; set; }    
}