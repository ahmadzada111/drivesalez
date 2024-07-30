using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByColorsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _colorsIds;

    public AnnouncementByColorsSpecification(List<int>? colorsIds)
    {
        _colorsIds = colorsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _colorsIds != null || _colorsIds.Contains(a.Vehicle.VehicleDetail.Color.Id);
    }
}