using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class MenuItemRepository : GenericRepositoryAsync<MenuItem>, IMenuItemRepository
{
    private readonly AppDbContext db;

    public MenuItemRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<List<MenuItem>> GetByMenuIdAsync(int menuId)
    {
        return await db.MenuItems.Where(x => x.MenuId == menuId).OrderBy(item => item.Position).ToListAsync();
    }
}
