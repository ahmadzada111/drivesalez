namespace DriveSalez.SharedKernel.DTO.UserDTO;

public record RegisterAccountDto
{
	public string? FirstName { get; init; }
	    
	public string? LastName { get; init; }
	    
	public string? BusinessName { get; init; }
	
	public string Email { get; init; }
	    
	public string? PhoneNumber { get ; init; }
	
	public ICollection<string>? PhoneNumbers { get; init; }
	
	public string? Address { get; set; }

	public string? Description { get; set; }
    
	public ICollection<WorkHourDto>? WorkHours { get; set; }
	
	public string Password { get; init; }
        
	public string ConfirmPassword { get; init; }
	
	public string UserType { get; init; }
}