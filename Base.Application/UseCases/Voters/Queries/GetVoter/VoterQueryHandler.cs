using Base.Domain.Entities;
using Base.Domain.Ports;

namespace Base.Application.UseCases.Voters.Queries.GetVoter;

public class VoterQueryHandler : IRequestHandler<VoterQuery, List<VoterDto>>
{
    private readonly IGenericRepository<Voter> _repository;
    private readonly IMapper _mapper;

    public VoterQueryHandler(IGenericRepository<Voter> repository, IMapper mapper) =>
        (_repository, _mapper) = (repository, mapper);


    public async Task<List<VoterDto>> Handle(VoterQuery request, CancellationToken cancellationToken)
    {
        var voter = (await _repository.GetAsync()).ToList();
        return _mapper.Map<List<VoterDto>>(voter);
    }
}