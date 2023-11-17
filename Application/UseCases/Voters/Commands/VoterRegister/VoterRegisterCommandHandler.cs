using Application.UseCases.Voters.Queries.GetVoter;
using Domain.Entities;
using Domain.Services;

namespace Application.UseCases.Voters.Commands.VoterRegister;

public class VoterRegisterCommandHandler : IRequestHandler<VoterRegisterCommand, VoterDto>
{
    private readonly RecordVoterService _service;
    private readonly IMapper _mapper;

    public VoterRegisterCommandHandler(RecordVoterService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<VoterDto> Handle(VoterRegisterCommand request, CancellationToken cancellationToken)
    {
        var voter = new Voter(request.Nid, request.Dob, request.Origin);
        voter.Test = new Test("hola");
        var voterSaved =   await _service.RecordVoterAsync(
            voter
        );
        return _mapper.Map<VoterDto>(voterSaved);
    }
}