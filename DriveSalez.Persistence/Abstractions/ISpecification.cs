using System.Linq.Expressions;

namespace DriveSalez.Persistence.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}