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

    public async Task DeletePCbyPostIdAsync(int postId)
    {
        List<PostCategory> pc = await db.PostCategories.Where(x => x.PostId == postId).ToListAsync();

        if (pc.Any())
        {
            db.PostCategories.RemoveRange(pc);
            await db.SaveChangesAsync();
        }

    }

    public async Task<List<PostCategory>> GetCategoryPosts(int postId)
    {
        return await db.PostCategories.Where(x => x.PostId == postId).ToListAsync();
    }
}
