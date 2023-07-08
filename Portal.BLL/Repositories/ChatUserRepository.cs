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


}
