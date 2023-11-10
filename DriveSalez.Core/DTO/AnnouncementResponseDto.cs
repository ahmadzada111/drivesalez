using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO;

public class AnnouncementResponseDto
{
    public Guid Id { get; set; }
    
    public Vehicle Vehicle { get; set; }

    public bool? Barter { get; set; }

    public bool? OnCredit { get; set; }

    public string? Description { get; set; }
      
    public decimal Price { get; set; }    

    public Currency Currency { get; set; }

    public AnnouncementState AnnouncementState { get; set; }

    public List<ImageUrl>? ImageUrls { get; set; }
        
    public Country Country { get; set; }

    public City City { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }

    public Guid UserId { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public List<AccountPhoneNumber> PhoneNumbers { get; set; }
}