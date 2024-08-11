namespace DriveSalez.SharedKernel.Settings;

public class EmailSettings
{
    public string CompanyEmail { get; set; } = string.Empty;
    
    public string SenderName { get; set; } = string.Empty;
    
    public string EmailKey { get; set; } = string.Empty;
    
    public string SmtpServer { get; set; } = string.Empty;

    public int Port { get; set; }
}