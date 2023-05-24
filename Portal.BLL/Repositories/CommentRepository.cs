using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class CommentRepository : GenericRepositoryAsync<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext db) : base(db)
    {
    }
}
