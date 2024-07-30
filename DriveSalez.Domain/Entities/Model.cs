using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class Model
{
    public int Id { get; set; }
        
    public string Title { get; set; }

    [JsonIgnore]
    public Make Make { get; set; }
    
    [JsonIgnore]
    public List<Vehicle> Vehicles { get; } = [];
}