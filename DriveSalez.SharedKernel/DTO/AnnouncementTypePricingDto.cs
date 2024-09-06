namespace DriveSalez.SharedKernel.DTO;

public class AnnouncementTypePricingDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public string PricingOptionType { get; set; }

    public decimal Price { get; set; }
}