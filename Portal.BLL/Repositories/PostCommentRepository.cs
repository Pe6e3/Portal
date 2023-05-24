using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostCommentRepository : GenericRepositoryAsync<PostComment>, IPostCommentRepository
{
    public PostCommentRepository(AppDbContext db) : base(db)
    {
    }
}
