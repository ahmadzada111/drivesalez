namespace DriveSalez.SharedKernel.Settings;

public class JwtSettings
{
    public string Secret { get; set; }
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
    
    public int Expiration { get; set; }
}