using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnounecementByMakeSpecification : ISpecification<Announcement>
{
    private readonly int? makeId;
    
    public bool IsSatisfied(Announcement item)
    {
        return !makeId.HasValue || item.Vehicle.Make.Id == makeId;
    }
}