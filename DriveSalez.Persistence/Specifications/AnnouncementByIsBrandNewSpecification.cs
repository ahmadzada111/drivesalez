using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByIsBrandNewSpecification : ISpecification<Announcement>
{
    private readonly bool? _isBrandNew;

    public AnnouncementByIsBrandNewSpecification(bool? isBrandNew)
    {
        _isBrandNew = isBrandNew;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => !_isBrandNew.HasValue || a.Vehicle.IsBrandNew == _isBrandNew;
    }
}