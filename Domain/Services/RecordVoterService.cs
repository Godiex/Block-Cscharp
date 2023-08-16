using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;

namespace Domain.Services;

[DomainService]
public class RecordVoterService
{
    private readonly IGenericRepository<Voter> _voterRepository;

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
            throw new UnderAgeException(Messages.UnderAgeException);
        }
    }

    private void CheckOrigin(Voter v)
    {
        if (!v.CanVoteBasedOnLocation)
        {
            throw new WrongCountryException(Messages.WrongCountryException);
        }
    }
}
