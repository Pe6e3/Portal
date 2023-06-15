using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
{
    private readonly AppDbContext db;

    public UserRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<bool> UserCheck(string login, string? email) => await db.Users.AnyAsync(u => u.Email == email || u.Login == login);
}
