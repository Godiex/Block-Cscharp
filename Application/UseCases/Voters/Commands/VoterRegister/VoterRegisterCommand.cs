using Application.UseCases.Voters.Queries.GetVoter;

namespace Application.UseCases.Voters.Commands.VoterRegister;

public record VoterRegisterCommand(
    string Nid, string Origin, DateTime Dob
) : IRequest<VoterDto>;
