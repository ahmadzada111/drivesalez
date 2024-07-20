namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateCurrencyDto
{
    public int CurrencyId { get; init; }
    
    public string NewCurrencyName { get; init; }
}