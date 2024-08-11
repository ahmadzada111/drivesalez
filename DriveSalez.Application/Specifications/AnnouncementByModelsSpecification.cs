using System.Linq.Expressions;
using DriveSalez.Application.Abstractions;
using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByModelsSpecification : ISpecification<Announcement>
{
    private readonly List<int>? _modelsIds;

    public AnnouncementByModelsSpecification(List<int>? modelsIds)
    {
        _modelsIds = modelsIds;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _modelsIds == null || _modelsIds.Contains(a.Vehicle.Model.Id);
    }
}