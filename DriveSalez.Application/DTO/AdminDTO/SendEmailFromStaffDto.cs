namespace DriveSalez.Application.DTO.AdminDTO;

public record SendEmailFromStaffDto
{
    public string ToEmail { get; init; }
    
    public string Subject { get; init; }
    
    public string Body { get; init; }
}