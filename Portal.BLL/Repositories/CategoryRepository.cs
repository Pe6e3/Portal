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


    // этот метод перебирает все категории и находит ту, у которой тот слаг, по которому мы ее ищем. Затем создает список из таблицы ПостКатегория, где ИД категории
    // такой же, как в найденной нами категории. К каждой сущности подгружается таблица Постов, затем таблица Категорий. Минус в том, что здесь 2 запроса в одном методе
    public async Task<List<PostCategory>> GetPostsByCatSlugAsync(string categorySlug)
    {
        Category? cat = await db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
        if (cat == null) return null;
        List<PostCategory>? pc = await db.PostCategories.Where(x => x.CategoryId == cat.Id)
            .Include(db => db.Category)
            .Include(db => db.Post)
            .ThenInclude(db => db.Content)
            .ToListAsync();

        return pc;
    }
}
