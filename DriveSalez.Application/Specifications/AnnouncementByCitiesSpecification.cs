using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByCitiesSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _citiesIds;

    public AnnouncementByCitiesSpecification(List<int>? citiesIds)
    {
        _citiesIds = citiesIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _citiesIds == null || _citiesIds.Contains(a.City.Id);
    }
}