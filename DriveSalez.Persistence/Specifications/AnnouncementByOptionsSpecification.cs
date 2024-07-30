using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByOptionsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _optionsIds;

    public AnnouncementByOptionsSpecification(List<int>? optionsIds)
    {
        _optionsIds = optionsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _optionsIds == null || !_optionsIds.Any()
                                           || a.Vehicle.VehicleDetail.Options.Any(c => _optionsIds.Contains(c.Id));
    }
}