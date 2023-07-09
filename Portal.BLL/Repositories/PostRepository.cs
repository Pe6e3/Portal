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

    public async Task<Post?> GetPostByIdAsync(int id) => await
        db.Posts
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Post?> GetPostByIdAsync(int id, string include1) => await
        db.Posts
        .Include(include1)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Post?> GetPostByIdAsync(int id, string include1, string include2) => await
        db.Posts
        .Include(include1).Include(include2)
        .FirstOrDefaultAsync(x => x.Id == id);



    public async Task<Post?> GetPostBySlug(string postSlug) => await
        db.Posts
        .Include(x => x.Categories)
        .Include(x => x.Content)
        .Include(x => x.Comments)
        .FirstOrDefaultAsync(post => post.Slug == postSlug);


    public async Task<IReadOnlyList<Post>?> ListAllPostsWithContentsAsync() => await
        db.Posts
        .Include(p => p.Content)
        .ToListAsync();

    public async Task<IEnumerable<Post>> ListLastPostsForColumn(int count)
    {
        List<Post> posts = await
            db.Posts
            .Include(p => p.Content)
            .OrderByDescending(x => x.Id)
            .Take(count)
            .ToListAsync();
        return posts;
    }

    public async Task<List<Post>> ListPostsWithComments(int count)
    {
        List<Post> posts = await db.Posts
            .Include(x => x.Content)
            .Take(count)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        List<Comment> comments = new List<Comment>();
        foreach (Post post in posts)
            post.Comments = await db.Comments.Where(x => x.PostId == post.Id).ToListAsync();

        return posts;

    }
}
