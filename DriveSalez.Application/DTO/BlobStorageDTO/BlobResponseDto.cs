namespace DriveSalez.Application.DTO.BlobStorageDTO;

public record BlobResponseDto
{
    public string? Status { get; init; }
    
    public bool? Error { get; init; }
    
    public BlobDto Blob { get; init; }
}