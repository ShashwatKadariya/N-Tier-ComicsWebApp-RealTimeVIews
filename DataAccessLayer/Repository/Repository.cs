using System.Linq.Expressions;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class Repository<T>: IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    
    
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    
    public IEnumerable<T>? GetAll(string? includeProperties = null)
    { 
        IQueryable<T> query = dbSet;
        if (!string.IsNullOrEmpty(includeProperties)) {
            foreach (var includeProp in includeProperties
                         .Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }

    public T? GetById(Guid id)
    {
        return dbSet.Find(id);
    }

    public T? Get(Expression<Func<T, bool>> predicate, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(predicate);
        
        if (!string.IsNullOrEmpty(includeProperties)) {
            foreach (var includeProp in includeProperties
                         .Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProp);
            }
        }
        
        return query.FirstOrDefault();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteMultiple(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}