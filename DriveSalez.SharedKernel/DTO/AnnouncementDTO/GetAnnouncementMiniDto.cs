namespace DriveSalez.SharedKernel.DTO.AnnouncementDTO;

public record GetAnnouncementMiniDto
{
    public Guid Id { get; init; }
    
    public string Make { get; init; }
    
    public string Model { get; init; }
    
    public decimal Price { get; init; }
    
    public bool IsPremium { get; init; }

    public bool Barter { get; init; }

    public bool OnCredit { get; init; }
    
    public string VinCode { get; init; }
    
    public int Mileage { get; init; }
    
    public string DistanceUnit { get; init; }
    
    public double EngineVolume { get; init; }
    
    public string FuelType { get; init; }
    
    public string Year { get; init; }
    
    public string Currency { get; init; }
    
    public List<string> ImageUrls { get; init; }
}