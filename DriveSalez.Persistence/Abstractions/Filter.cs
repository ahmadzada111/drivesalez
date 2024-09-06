namespace DriveSalez.Persistence.Abstractions;

public class Filter<T> : IFilter<T>
{
    public IQueryable<T> ApplyFilter(IQueryable<T> items, ISpecification<T> spec)
    {
        return items.Where(spec.ToExpression());
    }
}