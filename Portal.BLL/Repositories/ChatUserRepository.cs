using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class ChatUserRepository : GenericRepositoryAsync<ChatUser>, IChatUserRepository
{
    private readonly AppDbContext db;

    public ChatUserRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<List<ChatUser>> GetChatInfo(int chatId)
    {
        List<ChatUser>? chatUsers = await
            db.ChatUsers
            .Where(x => x.ChatId == chatId)
            .Include(x => x.Chat)
            .Include(x => x.User)
            .ThenInclude(u => u.Profile)
            .Include(x => x.User.Role)
            .ToListAsync();
        return chatUsers;
    }

    public async Task<bool> IsExist(int chatId, int userId)
    {
        ChatUser chatUser = await db.ChatUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.ChatId == chatId);
        return chatUser != null;
    }

    public async Task<List<ChatUser>> ListChatUsersofChat(int chatId)
    {
        List<ChatUser> chatUsers = await db.ChatUsers.Where(x=>x.ChatId == chatId).ToListAsync();
        return chatUsers;
    }
}
