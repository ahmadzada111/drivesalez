namespace DriveSalez.SharedKernel.DTO;

public class WorkHourDto
{
    public string DayOfWeek { get; set; }
    
    public TimeSpan? OpenTime { get; set; }
    
    public TimeSpan? CloseTime { get; set; } 
    
    public bool IsClosed { get; set; }
}