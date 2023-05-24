using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class MenuItemRepository : GenericRepositoryAsync<MenuItem>, IMenuItemRepository
{
    public MenuItemRepository(AppDbContext db) : base(db)
    {
    }
}
