using Application.Ports;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters.Repository;

public abstract class ReadRepositoryBase<T> : IReadRepository<T> where T : class
{
  private readonly DbContext dbContext;
  private readonly ISpecificationEvaluator specificationEvaluator;

  public ReadRepositoryBase(DbContext dbContext)
      : this(dbContext, SpecificationEvaluator.Default)
  {
  }
    
  public ReadRepositoryBase(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
  {
    this.dbContext = dbContext;
    this.specificationEvaluator = specificationEvaluator;
  }
 
    
  public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
  {
    return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
  }
    
  public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
  }
    
  public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
  }
    
  public virtual async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
  }
    
  public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
  }
    
  public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    return await dbContext.Set<T>().ToListAsync(cancellationToken);
  }
    
  public virtual async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

    return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
  }
    
  public virtual async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

    return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
  }
    
  public virtual async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification, true).CountAsync(cancellationToken);
  }
    
  public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    return await dbContext.Set<T>().CountAsync(cancellationToken);
  }
    
  public virtual async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
  }
    
  public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
    return await dbContext.Set<T>().AnyAsync(cancellationToken);
  }
  
  protected virtual IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
  {
    return specificationEvaluator.GetQuery(dbContext.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);
  }
  
  protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
  {
    return specificationEvaluator.GetQuery(dbContext.Set<T>().AsQueryable(), specification);
  }
}