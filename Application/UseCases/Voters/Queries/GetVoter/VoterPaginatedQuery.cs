using Application.Common.Helpers.Pagination;

namespace Application.UseCases.Voters.Queries.GetVoter;

public record VoterPaginatedQuery : PaginationRequest, IRequest<PaginationResponse<VoterDto>>
{
    public VoterPaginatedQuery(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
}
