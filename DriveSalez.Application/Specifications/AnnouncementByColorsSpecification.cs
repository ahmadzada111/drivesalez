using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByColorsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _colorsIds;

    public AnnouncementByColorsSpecification(List<int>? colorsIds)
    {
        _colorsIds = colorsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _colorsIds == null || _colorsIds.Contains(a.Vehicle.VehicleDetail.Color.Id);
    }
}