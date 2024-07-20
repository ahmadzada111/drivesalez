namespace DriveSalez.SharedKernel.Settings;

public class EmailSettings
{
    public string CompanyEmail { get; set; }
    
    public string SenderName { get; set; }
    
    public string EmailKey { get; set; }
    
    public string SmtpServer { get; set; }

    public int Port { get; set; }
}