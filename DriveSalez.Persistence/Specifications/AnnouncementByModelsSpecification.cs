using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByModelsSpecification : ISpecification<Announcement>
{
    private readonly int? modelId;
    
    public bool IsSatisfied(Announcement item)
    {
        return !modelId.HasValue || item.Vehicle.Model.Id == modelId;
    }
}