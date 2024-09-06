using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByBodyTypesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _bodyTypesIds;

    public AnnouncementByBodyTypesSpecification(List<int>? bodyTypesIds)
    {
        _bodyTypesIds = bodyTypesIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _bodyTypesIds == null ||
                    _bodyTypesIds.Contains(a.Vehicle.VehicleDetail.BodyType.Id);
    }
}