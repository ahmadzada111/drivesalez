using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByMileageRangeSpecification : ISpecification<Announcement>
{
    private readonly int? _fromMileage;
    private readonly int? _toMileage;

    public AnnouncementByMileageRangeSpecification(int? fromMileage, int? toMileage)
    {
        _fromMileage = fromMileage;
        _toMileage = toMileage;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => (_fromMileage.HasValue || a.Vehicle.VehicleDetail.Mileage >= _fromMileage)
                    && (_toMileage.HasValue || a.Vehicle.VehicleDetail.Mileage <= _toMileage);
    }
}