using Microsoft.EntityFrameworkCore;
using Portal.DAL.Data;
using Portal.DAL.Interfaces;

namespace Portal.BLL.Repositories;
public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    private readonly AppDbContext _db;
    public GenericRepositoryAsync(AppDbContext db)
    {
        _db = db;
    }
    public async Task<T> GetByIdAsync(int id) => await _db.Set<T>().FindAsync(id);



    public async Task<T> InsertAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
    }


    public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int size) => await _db.Set<T>()
                        .Skip((page - 1) * size)
                        .Take(size)
                        .ToListAsync();

    public async Task<IReadOnlyList<T>> ListAllAsync() => await _db.Set<T>().ToListAsync();
    public async Task<IReadOnlyList<T>> ListAllAsync(int count, string include) => await _db.Set<T>().Include(include).Take(count).ToListAsync();
    public async Task<IReadOnlyList<T>> ListAllWithIncludeAsync() => await _db.Set<T>().ToListAsync();

}
