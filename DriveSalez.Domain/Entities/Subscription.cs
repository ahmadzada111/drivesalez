using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    [JsonIgnore]
    public int PriceId { get; set; }

    public PriceDetail Price { get; set; }
}