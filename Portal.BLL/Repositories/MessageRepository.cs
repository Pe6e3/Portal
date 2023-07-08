using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class MessageRepository : GenericRepositoryAsync<Message>, IMessageRepository
{
    private readonly AppDbContext db;

    public MessageRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

 
}
