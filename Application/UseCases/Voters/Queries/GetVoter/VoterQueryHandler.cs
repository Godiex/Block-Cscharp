using Application.Common.Helpers.Pagination;
using Application.Ports;
using Domain.Entities;

namespace Application.UseCases.Voters.Queries.GetVoter;

public class VoterQueryHandler : IRequestHandler<VoterPaginatedQuery, PaginationResponse<VoterDto>>
{
    private readonly IReadRepository<Voter> _repository;

    public VoterQueryHandler(IReadRepository<Voter> repository) =>
        _repository = repository;


    public async Task<PaginationResponse<VoterDto>> Handle(VoterPaginatedQuery query, CancellationToken cancellationToken)
    {
        var spec = new GetVotersSpec();
        var votersPaginated = await _repository.PaginatedListAsync(spec, query.PageNumber, query.PageSize, cancellationToken);
        return votersPaginated;
    }
}