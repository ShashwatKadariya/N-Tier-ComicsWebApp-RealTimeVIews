using DataAccess.Entities;

namespace DataAccess.Repository.IRepository;

public interface IComicRepository: IRepository<Comic>
{
    void Update(Comic comic);
}