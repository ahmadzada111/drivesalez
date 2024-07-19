namespace DriveSalez.Application.DTO;

public class GetModeratorDto
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
}