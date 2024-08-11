using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class PricingOption
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public PricingOptionType PricingOptionType { get; set; }

    public decimal Price { get; set; }
}