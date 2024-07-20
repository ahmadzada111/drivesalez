namespace DriveSalez.Application.DTO.AccountDTO;

public record PaymentRequestDto
{
    public string CardNumber { get; init; }
        
    public string Cvv { get; init; }
    
    public int ExpireMonth { get; init; }
    
    public int ExpireYear { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public decimal Sum { get; init; }
}