using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByGearboxesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _gearBoxIds;

    public AnnouncementByGearboxesSpecification(List<int>? gearBoxIds)
    {
        _gearBoxIds = gearBoxIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _gearBoxIds == null 
                    || _gearBoxIds.Contains(a.Vehicle.VehicleDetails.GearboxType.Id);
    }
}