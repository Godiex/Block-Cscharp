namespace Application.UseCases.Voters.Queries.GetVoter;

public record VoterDto(Guid Id, DateTime DateOfBirth, string Origin);