namespace DriveSalez.Core.DTO;

public class BlobResponseDto
{
    public string? Status { get; set; }
    
    public bool? Error { get; set; }
    
    public BlobDto Blob { get; set; }
    
    public BlobResponseDto(BlobDto blob)
    {
        Blob = blob;
    }
}