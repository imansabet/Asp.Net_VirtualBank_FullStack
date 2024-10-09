using Domain.Contracts;

namespace Application.Repositories;

public interface IWriteRepositoryAsync<T,in TId> where T: class,IEntity<TId>
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
  