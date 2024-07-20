namespace DriveSalez.Application.DTO.AdminDTO;

public record UpdateDrivetrainDto
{
    public int DrivetrainId { get; init; } 
    
    public string NewDrivetrain { get; init; }
}