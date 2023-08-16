namespace Application.UseCases.Voters.Queries.GetVoter;

public record VoterQuery : IRequest<List<VoterDto>>;
