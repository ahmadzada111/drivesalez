using DriveSalez.Core.Entities;

namespace DriveSalez.Core.DTO;

public class PaymentRequestDto
{
    public string CardNumber { get; set; }
        
    public string Cvv { get; set; }
    
    public int ExpireMonth { get; set; }
    
    public int ExpireYear { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int SubscriptionId { get; set; }
}