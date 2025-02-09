using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;

public class UnitOfWork: IUnitOfWork
{
    public IComicRepository Comic { get; private set; }
    public ICategoryRepository Category { get; private set; }
    public ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        this._db = db;
        Comic = new ComicRepository(_db);
        Category = new CategoryRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}