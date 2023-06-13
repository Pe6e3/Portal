using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostRepository : GenericRepositoryAsync<Post>, IPostRepository
{
    private readonly AppDbContext db;

    public PostRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<int> GetPostIdBySlugAsync(string postSlug)
    {
        var post = await db.Posts.FirstOrDefaultAsync(post => post.Slug == postSlug);
        return post != null ? post.Id : 0;
    }



    public async Task<IReadOnlyList<Post>?> ListAllPostsWithContentsAsync() => await db.Posts.Include(p => p.Content).ToListAsync();
}
