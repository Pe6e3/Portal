using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostContentRepository : GenericRepositoryAsync<PostContent>, IPostContentRepository
{
    public PostContentRepository(AppDbContext db) : base(db)
    {
    }
}
