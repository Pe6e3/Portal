using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;

public class MyLoggerRepository : GenericRepositoryAsync<MyLogger>, IMyLoggerRepository
{
    private readonly AppDbContext db;

    public MyLoggerRepository(AppDbContext db) : base(db)
    {
        this.db = db;
    }

}
