using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostContentRepository : GenericRepositoryAsync<PostContent>, IPostContentRepository
{
    private readonly AppDbContext db;

    public PostContentRepository(AppDbContext db) : base(db)
    {
       this. db = db;
    }

    public async Task DeleteContentByPostId(int postId)
    {
        PostContent? postContent = await
            db.PostContents
            .Include(x => x.Post)
            .FirstOrDefaultAsync(x => x.Post.Id == postId);
        if (postContent != null)
        {
            db.PostContents.Remove(postContent);
            await db.SaveChangesAsync();
        }
    }

    public async Task<PostContent> GetContentByPostIdAsync(int PostId) => await db.PostContents.Where(x => x.PostId == PostId).FirstOrDefaultAsync();

}
