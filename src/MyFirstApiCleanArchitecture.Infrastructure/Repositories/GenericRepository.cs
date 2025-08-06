using Microsoft.EntityFrameworkCore;
using MyFirstApiCleanArchitecture.Domain.Abstraction;

namespace MyFirstApiCleanArchitecture.Infrastructure.Repositories;

public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context = context;

    public IQueryable<TEntity> GetAll() //Return query to get lazy load
        => _context
            .Set<TEntity>()
            .AsNoTracking()//to fast and no track because not needed
            .AsQueryable();

    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await _context
                    .Set<TEntity>()//set entity that need
                    .FindAsync([id, cancellationToken], cancellationToken);

    public async Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _context
                 .Set<TEntity>()
                 .AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task CreateRangeAsync(
        IEnumerable<TEntity> entityCollection,
        CancellationToken cancellationToken = default)
        => await _context
                    .Set<TEntity>()
                    .AddRangeAsync(entityCollection, cancellationToken);

    public TEntity Update(TEntity entity)
    {
        _context
            .Set<TEntity>()
            .Update(entity);

        return entity;
    }

    public void UpdateRange(IEnumerable<TEntity> entityCollection)
        => _context
               .Set<TEntity>()
               .UpdateRange(entityCollection);

    public void Delete(TEntity entity)
        => _context
              .Set<TEntity>()
              .Remove(entity);
    

    public void DeleteRange(IEnumerable<TEntity> entityCollection) 
        => _context 
              .Set<TEntity>()
              .RemoveRange(entityCollection);

}
