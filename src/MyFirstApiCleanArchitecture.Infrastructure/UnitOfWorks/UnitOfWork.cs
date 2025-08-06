using Microsoft.EntityFrameworkCore;
using MyFirstApiCleanArchitecture.Domain.Abstraction;
using MyFirstApiCleanArchitecture.Infrastructure.Repositories;

namespace MyFirstApiCleanArchitecture.Infrastructure.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;//DI for DbContext

    public async Task<string> CommitAsync(
        CancellationToken cancellationToken = default, 
        bool checkForConcurrency = false)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);//async save
        }

        catch (DbUpdateConcurrencyException) when (checkForConcurrency)//если при обновлениии возникла ошибка из-за работы параллелизма
        {
            return "A concurrency conflict occured while saving changes";
        }
        return string.Empty;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() //return new Repository
        where TEntity : BaseEntity
    => new GenericRepository<TEntity>(_context);    
    
}
