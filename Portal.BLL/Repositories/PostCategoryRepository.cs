using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostCategoryRepository : GenericRepositoryAsync<PostCategory>, IPostCategoryRepository
{
    private readonly AppDbContext db;

    public PostCategoryRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task DeletePCbyPostId(int postId)
    {
        List<PostCategory> pc = await db.PostCategories.Where(x => x.PostId == postId).ToListAsync();

        if (pc.Any())
        {
            db.PostCategories.RemoveRange(pc);
            await db.SaveChangesAsync();
        }

    }

    public async Task<Category?> GetCatByPostId(int postId)
    {
        PostCategory? pc = await db.PostCategories.Include(c => c.Category).FirstOrDefaultAsync(x => x.PostId == postId);
        Category? cat = pc?.Category;
        return cat;
    }

    public async Task<List<PostCategory>> GetCategoryPosts(int postId) => await
        db.PostCategories
        .Where(x => x.PostId == postId)
        .ToListAsync();
    public async Task<List<PostCategory>> GetCategoryPosts(string categorySlug, int count) => await
        db.PostCategories
        .Include(p => p.Post.Comments)
        .Include(p => p.Post.Content)
        .Include(p => p.Category)
        .Where(x => x.Category.Slug == categorySlug)
        .OrderByDescending(x => x.Id)
        .Take(count)
        .ToListAsync();

    public async Task<List<PostCategory>> GetCategoryPosts(string categorySlug, int count, int skip) => await
    db.PostCategories
    .Include(p => p.Post)
    .ThenInclude(c => c.Content)
    .Where(x => x.Category.Slug == categorySlug)
    .OrderByDescending(x => x.Id)
    .Skip(skip)
    .Take(count)
    .ToListAsync();





}
