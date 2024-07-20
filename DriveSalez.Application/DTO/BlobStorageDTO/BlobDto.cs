namespace DriveSalez.Application.DTO.BlobStorageDTO;

public record BlobDto
{
    public string? Url { get; init; }
    
    public string? Name { get; init; }
    
    public string? ContentType { get; init; }
    
    public Stream? Content { get; init; }
}