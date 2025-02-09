using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category category)
    {
        _db.Update(category);
    }
}