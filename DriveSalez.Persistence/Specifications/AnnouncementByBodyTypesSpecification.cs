using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByBodyTypesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _bodyTypeIds;

    public AnnouncementByBodyTypesSpecification(List<int>? bodyTypeIds)
    {
        _bodyTypeIds = bodyTypeIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _bodyTypeIds != null ||
                    _bodyTypeIds.Contains(a.Vehicle.VehicleDetails.BodyType.Id);
    }
}