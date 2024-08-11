using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByConditionsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _conditionsIds;

    public AnnouncementByConditionsSpecification(List<int>? conditionsIds)
    {
        _conditionsIds = conditionsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _conditionsIds == null || _conditionsIds.Any()
                                           || a.Vehicle.VehicleDetail.Conditions.Any(c => _conditionsIds.Contains(c.Id));
    }
}