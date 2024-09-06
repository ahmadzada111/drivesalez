using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementBySeatCountSpecification : ISpecification<Announcement>
{
    private readonly int? _seatCount;

    public AnnouncementBySeatCountSpecification(int? seatCount)
    {
        _seatCount = seatCount;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _seatCount.HasValue || a.Vehicle.VehicleDetail.SeatCount == _seatCount;
    }
}