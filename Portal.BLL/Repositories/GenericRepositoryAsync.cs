using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;
public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
{
    private readonly AppDbContext db;
    public GenericRepositoryAsync(AppDbContext db)
    {
        this.db = db;
    }
    public async Task<T?> GetByIdAsync(int id) => await
        db.Set<T>().FindAsync(id);
    public async Task<T?> GetByIdAsync(int id, string include1) => await
        db.Set<T>()
        .Include(include1)
        .FirstOrDefaultAsync(x => x.Id == id);
    public async Task<T?> GetByIdAsync(int id, string include1, string include2) => await
        db.Set<T>()
        .Include(include1)
        .Include(include2)
        .FirstOrDefaultAsync(x => x.Id == id);
    public async Task<T?> GetByIdAsync(int id, string include1, string include2, string include3) => await
        db.Set<T>()
        .Include(include1)
        .Include(include2)
        .Include(include3)
        .FirstOrDefaultAsync(x => x.Id == id);



    public async Task<T> InsertAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync();
    }


    public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int size) => await db.Set<T>()
                        .Skip((page - 1) * size)
                        .Take(size)
                        .ToListAsync();


    public async Task<IReadOnlyList<T>> ListAllDesc() => await
    db.Set<T>()
    .OrderByDescending(x => x.Id)
    .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAllDesc(string include) => await
    db.Set<T>()
    .Include(include)
    .OrderByDescending(x => x.Id)
    .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAll() => await
        db.Set<T>()
        .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAll(int count, string include) => await
        db.Set<T>()
        .Include(include)
        .Take(count)
        .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAll(int count, string include, string include2) => await
        db.Set<T>()
        .Include(include)
        .Include(include2)
        .Take(count)
        .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAll(string include) => await
    db.Set<T>()
    .Include(include)
    .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAll(string include, string include2) => await
    db.Set<T>()
    .Include(include)
    .Include(include2)
    .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAllWithInclude() => await db.Set<T>().ToListAsync();

}
