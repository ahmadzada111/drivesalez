using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByCountrySpecification : ISpecification<Announcement>
{
    private readonly int? _countryId;

    public AnnouncementByCountrySpecification(int? countryId)
    {
        _countryId = countryId;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _countryId.HasValue || a.Country.Id == _countryId;
    }
}