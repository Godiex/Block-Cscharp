using Base.Application.UseCases.Voters.Queries.GetVoter;
using Base.Domain.Entities;
using Base.Domain.Services;

namespace Base.Application.UseCases.Voters.Commands;

public class VoterRegisterCommandHandler : IRequestHandler<VoterRegisterCommand, VoterDto>
{
    private readonly RecordVoterService _service;

    public VoterRegisterCommandHandler(RecordVoterService service) =>
        _service = service ?? throw new ArgumentNullException(nameof(service));


    public async Task<VoterDto> Handle(VoterRegisterCommand request, CancellationToken cancellationToken)
    {
        var voterSaved =   await _service.RecordVoterAsync(
            new Voter(request.Nid, request.Dob, request.Origin)
        );

        return new VoterDto(voterSaved.Id, voterSaved.DateOfBirth, voterSaved.Origin);
    }
}