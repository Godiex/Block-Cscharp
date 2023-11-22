using Application.UseCases.Voters.Queries.GetVoter;
using Domain.Entities;
using Domain.Services;
using Mapster;

namespace Application.UseCases.Voters.Commands.VoterRegister;

public class VoterRegisterCommandHandler : IRequestHandler<VoterRegisterCommand, VoterDto>
{
    private readonly RecordVoterService _service;

    public VoterRegisterCommandHandler(RecordVoterService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task<VoterDto> Handle(VoterRegisterCommand request, CancellationToken cancellationToken)
    {
        var voter = new Voter(request.Nid, request.Dob, request.Origin)
        {
            Test = new Test("hola")
        };
        var voterSaved =   await _service.RecordVoterAsync(
            voter
        );
        return voterSaved.Adapt<VoterDto>();
    }
}