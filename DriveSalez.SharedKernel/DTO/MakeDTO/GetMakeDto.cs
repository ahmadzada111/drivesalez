using DriveSalez.SharedKernel.DTO.ModelDTO;

namespace DriveSalez.SharedKernel.DTO.MakeDTO;

public class GetMakeDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public List<ModelDto>? Models { get; set; }
}