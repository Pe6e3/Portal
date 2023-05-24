using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db)
    {
    }
}
