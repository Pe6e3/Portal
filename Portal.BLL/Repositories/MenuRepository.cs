using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class MenuRepository : GenericRepositoryAsync<Menu>, IMenuRepository
{
    public MenuRepository(AppDbContext db) : base(db)
    {
    }
}
