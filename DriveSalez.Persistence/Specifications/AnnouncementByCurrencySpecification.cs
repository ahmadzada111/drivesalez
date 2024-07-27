using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByCurrencySpecification : ISpecification<Announcement>
{
    private readonly int? _currencyId;

    public AnnouncementByCurrencySpecification(int? currencyId)
    {
        _currencyId = currencyId;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => !_currencyId.HasValue || a.Currency.Id == _currencyId;
    }
}