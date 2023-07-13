using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class ChatRepository : GenericRepositoryAsync<Chat>, IChatRepository
{
    private readonly AppDbContext db;

    public ChatRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Chat>> GetAllChats()
    {
        return await db.Chats
            .Include(x=>x.Users)
            .ToListAsync();
    }
}
