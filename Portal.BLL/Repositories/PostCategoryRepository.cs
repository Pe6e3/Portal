using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostCategoryRepository : GenericRepositoryAsync<PostCategory>, IPostCategoryRepository
{
    public PostCategoryRepository(AppDbContext db) : base(db)
    {
    }
}
