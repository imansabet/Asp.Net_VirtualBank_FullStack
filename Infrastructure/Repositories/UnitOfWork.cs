using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Context;
using System.Collections;

namespace Infrastructure.Repositories;

public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private readonly ApplicationDbContext _context;
    private bool disposed;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> CommitAsuync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }


    public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
    {
        if(_repositories == null)
        {
            _repositories = new Hashtable();
        }
        var type = typeof(T).Name;


        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(ReadRepositoryAsync<,>);
            var repositoryInstance = 
                Activator
                .CreateInstance(repositoryType.MakeGenericType(typeof(T),typeof(TId)),_context);

            _repositories.Add(type, repositoryInstance);
        }
        return (IReadRepositoryAsync<T, TId>)_repositories[type];
    }

    public IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }
        var type = typeof(T).Name;


        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(WriteRepositoryAsync<,>);
            var repositoryInstance =
                Activator
                .CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);

            _repositories.Add(type, repositoryInstance);
        }
        return (WriteRepositoryAsync<T, TId>)_repositories[type];
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
