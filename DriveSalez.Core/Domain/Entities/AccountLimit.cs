using DriveSalez.Core.DTO.Enums;
using Google.Api;

namespace DriveSalez.Core.DTO;

public class AccountLimit
{
    public int Id { get; set; }
    
    public int PremiumAnnouncementsLimit { get; set; }
    
    public int RegularAnnouncementsLimit { get; set; }
    
    public UserType UserType { get; set; }
}