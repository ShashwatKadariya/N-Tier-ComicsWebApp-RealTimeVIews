using System.Linq.Expressions;
using BusinessLogicLayer.Service.IService;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;

namespace BusinessLogicLayer.Service;

public class CategoryService: ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IEnumerable<Category>? GetAll()
    {
       return _unitOfWork.Category.GetAll();
    }

    public Category? GetById(Guid id)
    {
        return _unitOfWork.Category.GetById(id);
    }

    public Category? Get(Expression<Func<Category, bool>> predicate)
    {
        return _unitOfWork.Category.Get(predicate);
    }

    public void Add(Category entity)
    {
        _unitOfWork.Category.Add(entity);
        _unitOfWork.Save();
    }

    public void Delete(Category entity)
    {
        _unitOfWork.Category.Delete(entity);
        _unitOfWork.Save();
    }

    public void DeleteMultiple(IEnumerable<Category> entity)
    {
        _unitOfWork.Category.DeleteMultiple(entity);
        _unitOfWork.Save();
    }

    public void Update(Category entity)
    {
        _unitOfWork.Category.Update(entity);
        _unitOfWork.Save();
    }
}