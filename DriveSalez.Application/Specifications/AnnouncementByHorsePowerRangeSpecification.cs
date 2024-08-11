using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByHorsePowerRangeSpecification : ISpecification<Announcement>
{
    private readonly int? _fromHorsePower;
    private readonly int? _toHorsePower;

    public AnnouncementByHorsePowerRangeSpecification(int? fromHorsePower, int? toHorsePower)
    {
        _fromHorsePower = fromHorsePower;
        _toHorsePower = toHorsePower;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => (_fromHorsePower.HasValue || a.Vehicle.VehicleDetail.HorsePower >= _fromHorsePower)
                    && (_toHorsePower.HasValue || a.Vehicle.VehicleDetail.HorsePower <= _toHorsePower);
    }
}