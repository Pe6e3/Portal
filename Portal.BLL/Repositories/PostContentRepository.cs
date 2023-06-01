using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class PostContentRepository : GenericRepositoryAsync<PostContent>, IPostContentRepository
{
    private readonly AppDbContext _db;

    public PostContentRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<PostContent> GetContentByPostIdAsync(int PostId) => await _db.PostContents.Where(x => x.PostId == PostId).FirstOrDefaultAsync();

}
