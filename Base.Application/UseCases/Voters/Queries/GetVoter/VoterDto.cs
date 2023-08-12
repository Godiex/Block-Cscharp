namespace Base.Application.UseCases.Voters.Queries.GetVoter;

public record VoterDto(Guid Id, DateTime dateOfBirth, string origin);