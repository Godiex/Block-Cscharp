using System.Linq.Expressions;
using Domain;
using Domain.Entities.Base;
using Domain.Ports;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Repository;

public class GenericRepository<E> : IGenericRepository<E> where E : DomainEntity
{
    private readonly PersistenceContext _context;
    public GenericRepository(PersistenceContext context)
    {
        _context = context;
    }

    public async Task<E> AddAsync(E entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), Messages.EntityCannotBeNull);
        _context.Set<E>().Add(entity);
        await CommitAsync();
        return entity;
    }

    public async Task DeleteAsync(ISoftDelete entity, bool deleteCascade = true)
    {
        if (entity is { DeletedOn: not null })
        {
            return;
        }

        entity.SetDelete();

        if (deleteCascade)
        {
            var relatedEntities = entity.GetType()
                .GetProperties()
                .Where(prop => typeof(ISoftDelete).IsAssignableFrom(prop.PropertyType))
                .Select(prop => prop.GetValue(entity) as ISoftDelete);

            foreach (var relatedEntity in relatedEntities)
            {
                if (relatedEntity is { DeletedOn: null } and not IAggregateRoot)
                {
                    await DeleteAsync(relatedEntity);
                }
            }
        }

        await CommitAsync();
    }

    public async Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "", bool isTracking = false)
    {
        IQueryable<E> query = _context.Set<E>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeStringProperties))
        {
            query = includeStringProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync().ConfigureAwait(false);
        }

        return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

    public async Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false, params Expression<Func<E, object>>[] includeObjectProperties)
    {
        IQueryable<E> query = _context.Set<E>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeObjectProperties != null)
        {
            query = includeObjectProperties.Aggregate(query, (current, include) => current.Include(include));
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

    public async Task<E?> GetByIdAsync(object? id)
    {
        return await _context.Set<E>().FindAsync(id);
    }

    public async Task<bool> Exist(Expression<Func<E, bool>> filter)
    {
        return await _context.Set<E>().AnyAsync(filter);
    }

    public async Task UpdateAsync(E? entity)
    {
        if (entity != null)
        {
            _context.Set<E>().Update(entity);
            await CommitAsync();
        }
    }

    private async Task CommitAsync()
    {
        _context.ChangeTracker.DetectChanges();

        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Property("LastModifiedOn").CurrentValue = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        await _context.CommitAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _context.Dispose();
    }
}