using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

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