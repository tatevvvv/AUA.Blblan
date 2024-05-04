using Blblan.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Blblan.Data.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Set();

    IQueryable<T> AsNoTracking();

    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<PagedResult<T>> GetPagedAsync(int skip, int take);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
