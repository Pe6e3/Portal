﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<List<MyLogger>> ListLogsWithoutMe()
    {
        List<MyLogger> logs = await
            db.MyLoggers
            .Include(x => x.User)
            .ThenInclude(x=>x.Profile)
            .OrderByDescending(x=>x.Id)
            .Where(x=>x.UserIP != "2.133.12.48")
            .ToListAsync();
            return logs;
    }

    public async Task<List<MyLogger>> ListLogs()
    {
        List<MyLogger> logs = await
            db.MyLoggers
            .Include(x => x.User)
            .ThenInclude(x => x.Profile)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        return logs;
    }

    public async Task<List<MyLogger>> ListLogs(string login)
    {
        List<MyLogger> logs = await
            db.MyLoggers
            .Include(x => x.User)
            .ThenInclude(x => x.Profile)
            .Where(x=>x.User.Login==login)
            .ToListAsync();
        return logs;
    }
}
