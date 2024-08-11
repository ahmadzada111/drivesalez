namespace DriveSalez.Application.DTO;

public record UpdateMarketVersionDto
{
    public int MarketVersionId { get; init; }
    
    public string NewMarketVersion { get; init; }
}