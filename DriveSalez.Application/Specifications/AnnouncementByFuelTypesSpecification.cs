using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByFuelTypesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _fuelsIds;

    public AnnouncementByFuelTypesSpecification(List<int>? fuelsIds)
    {
        _fuelsIds = fuelsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _fuelsIds == null || _fuelsIds.Contains(a.Vehicle.VehicleDetail.FuelType.Id);
    }
}