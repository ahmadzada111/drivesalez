using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByBarterSpecification : ISpecification<Announcement>
{
    private readonly bool? _barter;

    public AnnouncementByBarterSpecification(bool? barter)
    {
        _barter = barter;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _barter.HasValue || a.Barter == _barter;
    }
}