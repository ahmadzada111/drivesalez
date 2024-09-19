using System.Linq.Expressions;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext DbContext { get; }
    
    protected GenericRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public async Task<T?> FindById<TKey>(TKey id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstOrDefaultAsync(where);
    }

    public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>>? where = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>().AsNoTracking();
        
        if (where != null)
        {
            query = query.Where(where);
        }
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await DbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public T Add(T entity)
    {
        return DbContext.Set<T>().Add(entity).Entity;
    }

    public T Delete(T entity)
    {
        return DbContext.Set<T>().Remove(entity).Entity;
    }

    public T Update(T entity)
    {
        return DbContext.Set<T>().Update(entity).Entity;
    }
}