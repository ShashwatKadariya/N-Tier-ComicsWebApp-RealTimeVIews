using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data;

public interface IDbInitializer
{
    void Initialize();
}
