namespace DriveSalez.Application.Abstractions;

public interface IFilter<T>
{
    IQueryable<T> ApplyFilter(IQueryable<T> items, ISpecification<T> spec);
}