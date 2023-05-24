using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class RoleRepository : GenericRepositoryAsync<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext db) : base(db)
    {
    }
}
