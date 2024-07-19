namespace DriveSalez.Application.DTO;

public class BlobDto
{
    public string? Url { get; set; }
    
    public string? Name { get; set; }
    
    public string? ContentType { get; set; }
    
    public Stream? Content { get; set; }
}