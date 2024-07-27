using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Persistence.Specifications;

public class AnnouncementByModelsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _modelsIds;

    public AnnouncementByModelsSpecification(List<int>? modelsIds)
    {
        _modelsIds = modelsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _modelsIds != null || _modelsIds.Contains(a.Vehicle.Model.Id);
    }
}