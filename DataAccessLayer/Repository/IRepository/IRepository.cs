using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T>? GetAll(string? includeProperties = null);
    T? GetById(Guid id);
    T? Get(Expression<Func<T, bool>> predicate, string? includeProperties = null);
    void Add(T entity);
    void Delete(T entity);
    void DeleteMultiple(IEnumerable<T> entity);
}