using Blblan.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Blblan.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BlblanDbContext _context;

    public Repository(BlblanDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Set()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> AsNoTracking()
    {
        return Set().AsNoTracking();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Set().FindAsync(id);
    }

    public async Task<PagedResult<T>> GetPagedAsync(int skip, int take)
    {
        var totalCount = await Set().AsNoTracking().CountAsync();
        var items = await Set().AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return new PagedResult<T>()
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Set().AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Set().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Set().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
