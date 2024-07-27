using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByFuelTypesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _fuelsIds;

    public AnnouncementByFuelTypesSpecification(List<int>? fuelsIds)
    {
        _fuelsIds = fuelsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _fuelsIds == null || _fuelsIds.Contains(a.Vehicle.FuelType.Id);
    }
}