using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

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