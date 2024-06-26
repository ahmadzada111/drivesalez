using System.ComponentModel.DataAnnotations;

namespace DriveSalez.Core.DTO;

public class AddNewModelDto
{
    public int MakeId { get; set; }
    
    [Required(ErrorMessage = "Model name cannot be blank!")]
    public string ModelName { get; set; }
}