using System.Linq.Expressions;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> FindById<TKey>(TKey id);
    
    Task<T?> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    
    Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] includes);
    
    Task<IEnumerable<T>> GetAll();
    
    T Add(T entity);
    
    T Delete(T entity);
    
    T Update(T entity);
}