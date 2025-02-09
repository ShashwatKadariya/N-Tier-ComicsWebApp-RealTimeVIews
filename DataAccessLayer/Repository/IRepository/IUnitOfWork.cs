namespace DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    IComicRepository Comic { get; }
    ICategoryRepository Category { get; }

    void Save();
    Task SaveAsync();
}