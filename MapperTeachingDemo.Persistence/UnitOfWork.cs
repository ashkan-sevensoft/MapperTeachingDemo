using MapperTeachingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTeachingDemo.Persistence;
public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _dbContext;

    private readonly Dictionary<Type, object> _repo = new();

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext=dbContext;
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
         var type = typeof(T);

        if (!_repo.TryGetValue(type, out var repo))
        {
            repo  = new GenericRepository<T>(_dbContext);
            _repo[type]=repo;
        }

        return (IGenericRepository<T>)repo ;

    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        => await _dbContext.SaveChangesAsync(cancellation);
    

}
