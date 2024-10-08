using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByMileageTypeSpecification : ISpecification<Announcement>
{
    private readonly string? _mileageType;

    public AnnouncementByMileageTypeSpecification(string? mileageType)
    {
        _mileageType = mileageType;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _mileageType == null 
                    || a.Vehicle.VehicleDetail.DistanceUnit.ToString() == _mileageType;
    }
}