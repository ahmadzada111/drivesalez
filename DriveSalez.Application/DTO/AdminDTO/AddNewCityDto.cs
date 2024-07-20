namespace DriveSalez.Application.DTO;

public record AddNewCityDto
{
    public string City { get; init; } 
    
    public int CountryId { get; init; }
}