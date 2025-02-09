using System.Linq.Expressions;
using DataAccess.Entities;

namespace BusinessLogicLayer.Service.IService;

public interface IComicService
{ 
    IEnumerable<Comic>? GetAll(string? includeProperties = null);
    Comic? GetById(Guid id);
    Comic? Get(Expression<Func<Comic, bool>> predicate, string? includeProperties = null);
    void Add(Comic entity);
    void Delete(Comic entity);
    void DeleteMultiple(IEnumerable<Comic> entity);
    void Update(Comic entity);
    Task SaveChangesAsync();
}