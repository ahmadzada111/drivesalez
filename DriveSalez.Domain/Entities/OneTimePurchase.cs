using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class OneTimePurchase
{
    public int Id { get; set; }

    public string Name { get; set; } 
    
    public LimitType LimitType { get; set; }

    public int LimitValue { get; set; }

    public decimal Price { get; set; }
    
    public UserPurchase UserPurchase { get; set; }
}