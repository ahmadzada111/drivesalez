namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record UserProfileDto
{
    public Guid Id { get; init; }
    
    public string? FirstName { get; init; }
        
    public string? LastName { get; init; }
        
    public string Email { get; init; }
        
    public string? PhoneNumber { get; init; }
    
    public List<string>? PhoneNumbers { get; init; }
    
    public string? ProfilePhotoImageUrl { get; init; }
    
    public string? BackgroundPhotoImageUrl { get; init; }
    
    public List<WorkHourDto>? WorkHours { get; init; }
    
    public string UserRole { get; init; }
}