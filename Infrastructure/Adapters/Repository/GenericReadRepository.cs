using Ardalis.Specification;
using Domain.Entities.Base;
using Infrastructure.Context;
using Mapster;

namespace Infrastructure.Adapters.Repository;

public class GenericReadRepository<T> : ReadRepositoryBase<T>
    where T : DomainEntity
{
    public GenericReadRepository(PersistenceContext dbContext)
        : base(dbContext)
    {
    }

    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}