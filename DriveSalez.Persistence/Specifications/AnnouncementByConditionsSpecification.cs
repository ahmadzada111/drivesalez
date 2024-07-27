using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByConditionsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _conditionsIds;

    public AnnouncementByConditionsSpecification(List<int>? conditionsIds)
    {
        _conditionsIds = conditionsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _conditionsIds == null || !_conditionsIds.Any()
                                           || a.Vehicle.VehicleDetails.Conditions.Any(c => _conditionsIds.Contains(c.Id));
    }
}