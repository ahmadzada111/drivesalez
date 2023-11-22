using DriveSalez.Core.DTO.Enums;

namespace DriveSalez.Core.DTO;

public class PaidAccountLimit
{
    public int Id { get; set; }
    
    public int PremiumAnnouncementsLimit { get; set; }

    public UserType UserType { get; set; }
}