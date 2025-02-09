using System.Linq.Expressions;
using BusinessLogicLayer.Service.IService;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;

namespace BusinessLogicLayer.Service;

public class ComicService: IComicService
{
    private readonly IUnitOfWork _unitOfWork;
    public ComicService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IEnumerable<Comic>? GetAll(string? includeProperties = null)
    {
       return _unitOfWork.Comic.GetAll(includeProperties);
    }

    public Comic? GetById(Guid id)
    {
        return _unitOfWork.Comic.GetById(id);
    }

    public Comic? Get(Expression<Func<Comic, bool>> predicate, string? includeProperties = null)
    {
        return _unitOfWork.Comic.Get(predicate, includeProperties);
    }

    public void Add(Comic entity)
    {
        _unitOfWork.Comic.Add(entity);
        _unitOfWork.Save();
    }

    public void Delete(Comic entity)
    {
        _unitOfWork.Comic.Delete(entity);
        _unitOfWork.Save();
    }

    public void DeleteMultiple(IEnumerable<Comic> entity)
    {
        _unitOfWork.Comic.DeleteMultiple(entity);
        _unitOfWork.Save();
    }

    public void Update(Comic entity)
    {
        _unitOfWork.Comic.Update(entity);
        _unitOfWork.Save();
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
    
}