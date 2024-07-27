using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByHorsePowerSpecification : ISpecification<Announcement>
{
    private readonly int? _fromHorsePower;
    private readonly int? _toHorsePower;

    public AnnouncementByHorsePowerSpecification(int? fromHorsePower, int? toHorsePower)
    {
        _fromHorsePower = fromHorsePower;
        _toHorsePower = toHorsePower;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        
    }
}