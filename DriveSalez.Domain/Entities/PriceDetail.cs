using System.Text.Json.Serialization;

namespace DriveSalez.Domain.Entities;

public class PriceDetail
{
    public int Id { get; set; }
    
    public decimal Price { get; set; }

    [JsonIgnore]
    public List<Subscription> Subscriptions { get; } = []; 

    [JsonIgnore]
    public List<AnnouncementTypePricing> AnnouncementTypePricings { get; } = [];
}