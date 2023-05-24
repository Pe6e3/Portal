using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class UserProfileRepository : GenericRepositoryAsync<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext db) : base(db)
    {
    }
}
