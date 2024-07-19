using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class AccountLimit
{
    public int Id { get; set; }
    
    public int PremiumAnnouncementsLimit { get; set; }
    
    public int RegularAnnouncementsLimit { get; set; }
    
    public UserType UserType { get; set; }
}