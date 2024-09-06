using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByYearRangeSpecification : ISpecification<Announcement>
{
    private readonly int? _fromYearId;
    private readonly int? _toYearId;

    public AnnouncementByYearRangeSpecification(int? fromYearId, int? toYearId)
    {
        _fromYearId = fromYearId;
        _toYearId = toYearId;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => (_fromYearId.HasValue || a.Vehicle.VehicleDetail.Year.Id >= _fromYearId)
                    && (_toYearId.HasValue || a.Vehicle.VehicleDetail.Year.Id <= _toYearId);
    }
}
