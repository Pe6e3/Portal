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

    public async Task<List<Comment>?> GetCommentsByPostSlug(int postId) => await db.Comments.Where(c => c.PostId == postId).ToListAsync();
    
}
