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

    public async Task<Category?> GetCategoryBySlugAsync(string categorySlug) => await
        db.Categories
        .Include(x => x.Posts)
        .ThenInclude(x => x.Content)
        .FirstOrDefaultAsync(x => x.Slug == categorySlug);

    public async Task<string> GetCategorySlugByPostSlug(string postSlug)
    {
        PostCategory? pc = await db.PostCategories
        .Include(x => x.Post)
        .Include(x => x.Category)
        .FirstOrDefaultAsync(p => p.Post.Slug == postSlug);

        return pc?.Category?.Slug;
    }

    public async Task<List<PostCategory>> GetPostsByCatSlugAsync(string categorySlug)
    {
        Category? cat = await db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
        if (cat == null) return null;

        List<PostCategory>? pc = await
            db.PostCategories
            .Where(x => x.CategoryId == cat.Id)
            .Include(db => db.Category)
            .Include(db => db.Post)
            .ThenInclude(db => db.Content)
            .ToListAsync();
        return pc;
    }

    public async Task<Category?> RandomCatId()
    {
        Random random = new Random();
        var categoryCount = db.Categories.ToList().Count();
        var randomId = random.Next(1, categoryCount);
        return await db.Categories.FirstOrDefaultAsync(x => x.Id == randomId);
    }

}
