using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
{
    private readonly AppDbContext db;

    public CategoryRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }


    public async Task<Category> GetPostsByCatSlugAsync(string categorySlug) => await
        db.Categories
        .Include(x => x.Posts)
        //.ThenInclude(i => i.Contents)
        .Where(cat => cat.Slug == categorySlug).FirstOrDefaultAsync();
}
