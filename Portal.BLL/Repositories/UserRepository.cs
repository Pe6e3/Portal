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

    public async Task<List<User>> GetAllUsers() => await
        db.Users
        .Include(x => x.Profile)
        .Include(x => x.Role)
        .ToListAsync();

    public async Task<User> GetDefaultUser() => await
        db.Users
        .Include(u => u.Profile)
        .FirstOrDefaultAsync(u => u.Login == "Default");
    public async Task<int> GetDefaultUserId()
    {
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Login == "Default");
        return user.Id;
    }

    public async Task<User> GetUserByLogin(string login) => await
        db.Users
        .Include(u => u.Profile)
        .Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.Login == login);
 

    public string? HashPass(string? password) => BCrypt.Net.BCrypt.HashPassword(password);

    public async Task<bool> UserCheck(string login) => await db.Users.Include(u => u.Profile).AnyAsync(u => u.Login == login);

    public async Task<User> ValidateUser(string login, string? password)
    {
        User user = await db.Users.Include(u => u.Role).Include(u => u.Profile).FirstOrDefaultAsync(u => u.Login == login);
        if (user == null) return null;                                       // TODO: передать сообщение если null: пользователя с таким логином нет
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            user.Password = "";
            return user;
        }
        // TODO: передать сообщение если не null, но пароль пустой: неверный пароль

        return user;                                                         // Если дошли до сюда, значит все ок, и передаем найденного пользователя

    }
}
