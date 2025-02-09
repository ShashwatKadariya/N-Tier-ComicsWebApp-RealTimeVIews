using System.Linq.Expressions;
using DataAccess.Entities;

namespace BusinessLogicLayer.Service.IService;

public interface ICategoryService
{
    IEnumerable<Category>? GetAll();
    Category? GetById(Guid id);
    Category? Get(Expression<Func<Category, bool>> predicate);
    void Add(Category entity);
    void Delete(Category entity);
    void DeleteMultiple(IEnumerable<Category> entity);
    void Update(Category entity);
}