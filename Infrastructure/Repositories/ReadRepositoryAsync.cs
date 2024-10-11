using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReadRepositoryAsync<T, TId> : IReadRepositoryAsync<T, TId> where T : BaseEntity<TId>
{
    private readonly ApplicationDbContext _context;
    public ReadRepositoryAsync(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(TId id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public IQueryable<T> Entities => _context.Set<T>();

}
