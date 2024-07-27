using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByGearboxesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _gearBoxesIds;

    public AnnouncementByGearboxesSpecification(List<int>? gearBoxesIds)
    {
        _gearBoxesIds = gearBoxesIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _gearBoxesIds == null 
                    || _gearBoxesIds.Contains(a.Vehicle.VehicleDetails.GearboxType.Id);
    }
}