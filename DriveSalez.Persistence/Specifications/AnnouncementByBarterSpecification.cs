using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByBarterSpecification : ISpecification<Announcement>
{
    private readonly bool? _barter;

    public AnnouncementByBarterSpecification(bool? barter)
    {
        _barter = barter;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => !_barter.HasValue || a.Barter == _barter;
    }
}