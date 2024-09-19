namespace DriveSalez.SharedKernel.DTO;

public record FileUploadData
{
    public required Stream Stream { get; init; }
    
    public required string FileType { get; init; }
}