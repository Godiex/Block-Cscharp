using Base.Application.UseCases.Voters.Queries.GetVoter;

namespace Base.Application.UseCases.Voters.Commands;

public record VoterRegisterCommand(
    string Nid, string Origin, DateTime Dob
) : IRequest<VoterDto>;
