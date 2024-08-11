using System.Linq.Expressions;

namespace DriveSalez.Application.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}