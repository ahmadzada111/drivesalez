using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByDrivetrainsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _driveTrainsIds;

    public AnnouncementByDrivetrainsSpecification(List<int>? driveTrainsIds)
    {
        _driveTrainsIds = driveTrainsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _driveTrainsIds == null 
                    || _driveTrainsIds.Contains(a.Vehicle.VehicleDetail.DrivetrainType.Id);
    }
}