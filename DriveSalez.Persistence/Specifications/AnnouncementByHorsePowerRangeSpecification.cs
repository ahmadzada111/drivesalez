using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

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
        return a => (!_fromHorsePower.HasValue || a.Vehicle.VehicleDetails.HorsePower >= _fromHorsePower)
                    && (!_toHorsePower.HasValue || a.Vehicle.VehicleDetails.HorsePower <= _toHorsePower);
    }
}