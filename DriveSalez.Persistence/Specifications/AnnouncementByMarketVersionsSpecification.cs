using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByMarketVersionsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _marketVersionsIds;

    public AnnouncementByMarketVersionsSpecification(List<int>? marketVersionsIds)
    {
        _marketVersionsIds = marketVersionsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _marketVersionsIds != null
                    || _marketVersionsIds.Contains(a.Vehicle.VehicleDetail.MarketVersion.Id);
    }
}