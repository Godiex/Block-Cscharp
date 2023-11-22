using Application.Ports;
using Ardalis.Specification;

namespace Application.Common.Helpers.Pagination;

public static class PaginationResponseExtension
{
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        this IReadRepository<T> repository,
        ISpecification<T, TDestination> spec,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
        where T : class
        where TDestination : class
    {
        var list = await repository.ListAsync(spec, cancellationToken);
        var count = await repository.CountAsync(spec, cancellationToken);
        return new PaginationResponse<TDestination>(list, count, pageNumber, pageSize);
    }
}
