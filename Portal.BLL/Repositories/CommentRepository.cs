using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class CommentRepository : GenericRepositoryAsync<Comment>, ICommentRepository
{
    private readonly AppDbContext db;

    public CommentRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        Comment comment = await
            db.Comments
            .Include(x => x.User)
            .ThenInclude(x => x.Profile)
            .Include(x => x.Post)
            .ThenInclude(x => x.Content)
            .FirstOrDefaultAsync(x => x.Id == commentId);
        return comment;
    }

    public async Task<List<Comment>?> GetCommentsByPostId(int postId) => await
        db.Comments
        .Include(x => x.User)
        .ThenInclude(x => x.Profile)
        .Where(c => c.PostId == postId)
        .ToListAsync();

    public async Task<int> GetPostCommentsCount(int postId)
    {
        List<Comment> comments = await db.Comments.Where(x => x.PostId == postId).ToListAsync();
        return comments.Count;
    }

    public async Task<IEnumerable<Comment>> ListAllComments() => await
        db.Comments
        .Include(x => x.Post)
        .ThenInclude(x => x.Content)
        .Include(x => x.User)
        .ThenInclude(x => x.Profile)
        .OrderByDescending(x => x.Id)
        .ToListAsync();
}
