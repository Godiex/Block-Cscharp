using Base.Domain.Entities;
using Base.Domain.Exceptions;
using Base.Domain.Ports;

namespace Base.Domain.Services;

[DomainService]
public class RecordVoterService
{
    private readonly IGenericRepository<Voter> _voterRepository;
    const string VOTER_ORIGIN = "Colombia";

    public RecordVoterService(IGenericRepository<Voter> voterRepository) =>
        _voterRepository = voterRepository;

    public async Task<Voter> RecordVoterAsync(Voter v)
    {
        CheckOrigin(v);
        CheckAge(v);
        var returnVote = await _voterRepository.AddAsync(v);
        return returnVote;
    }

    private void CheckAge(Voter v)
    {
        if (v.IsUnderAge)
        {
            throw new UnderAgeException("Voter is not 18 years or older");
        }
    }

    private void CheckOrigin(Voter v)
    {
        if (!v.CanVoteBasedOnLocation)
        {
            throw new WrongCountryException($"Voter is not from {VOTER_ORIGIN}");
        }
    }
}
