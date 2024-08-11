using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByPriceRangeSpecification : ISpecification<Announcement>
{
    private readonly decimal? _fromPrice;
    private readonly decimal? _toPrice;

    public AnnouncementByPriceRangeSpecification(decimal? fromPrice, decimal? toPrice)
    {
        _fromPrice = fromPrice;
        _toPrice = toPrice;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => (_fromPrice.HasValue || a.Price >= _fromPrice)
            && (_toPrice.HasValue || a.Price <= _toPrice);
    }
}