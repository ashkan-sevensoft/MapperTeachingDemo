using System.Linq.Expressions;
using MapperTeachingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace MapperTeachingDemo.Persistence;

public  class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext DbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity ,CancellationToken cancellation)
    {
        await DbContext.Set<TEntity>().AddAsync(entity );
        //await DbContext.SaveChangesAsync(cancellation);
    }

    public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false,
        int pageSize = 10, int skip = 0)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();

        if (!tracking) query = query.AsNoTracking();

        return await query.Where(predicate)
            .OrderByDescending(e => e.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();

        if (!tracking) query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task HardDeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);
        if (entity is null) return;

        DbContext.Set<TEntity>().Remove(entity);
        //await DbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id, true);
        if (entity is null) return;

        entity.SetAsDeleted();
        //await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        //await DbContext.SaveChangesAsync();
    }
}
