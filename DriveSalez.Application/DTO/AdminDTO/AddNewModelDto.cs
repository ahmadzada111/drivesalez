using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Application.DTO;

public record AddNewModelDto
{
    public int MakeId { get; init; }
    
    [Required(ErrorMessage = "Model name cannot be blank!")]
    public string ModelName { get; init; }
}