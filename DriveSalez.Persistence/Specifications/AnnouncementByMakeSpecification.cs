using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByMakeSpecification : ISpecification<Announcement>
{
    private readonly int? _makeId;

    public AnnouncementByMakeSpecification(int? makeId)
    {
        _makeId = makeId;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _makeId.HasValue || a.Vehicle.Make.Id == _makeId;
    }
}