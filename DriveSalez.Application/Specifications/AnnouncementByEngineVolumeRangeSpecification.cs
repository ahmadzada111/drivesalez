using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByEngineVolumeRangeSpecification : ISpecification<Announcement>
{
    private readonly int? _fromVolume;
    private readonly int? _toVolume;

    public AnnouncementByEngineVolumeRangeSpecification(int? fromVolume, int? toVolume)
    {
        _fromVolume = fromVolume;
        _toVolume = toVolume;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => (_fromVolume.HasValue || a.Vehicle.VehicleDetail.EngineVolume >= _fromVolume)
                    && (_toVolume.HasValue || a.Vehicle.VehicleDetail.EngineVolume <= _toVolume);
    }
}