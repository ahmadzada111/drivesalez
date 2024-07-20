namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateMarketVersionDto
{
    public int MarketVersionId { get; init; }
    
    public string NewMarketVersion { get; init; }
}