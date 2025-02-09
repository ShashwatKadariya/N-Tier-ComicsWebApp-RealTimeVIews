using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class ComicRepository: Repository<Comic>, IComicRepository
{
    private ApplicationDbContext _db;
    public ComicRepository(ApplicationDbContext db): base(db)
    {
        this._db = db;
    }
    public void Update(Comic entity)
    { 
        _db.Comics.Update(entity);
    }
}