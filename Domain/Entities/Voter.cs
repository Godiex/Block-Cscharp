using Domain.Exceptions;

namespace Domain.Entities;

public class Voter : EntityBase<Guid>, IAggregateRoot
{
    private const int MinimumAge = 18;
    private const string CountryOfOrigin = "COLOMBIA";

    public Voter()
    {

    }

    public Voter(string nid, DateTime dateOfBirth, string origin)
    {
        Nid = nid.Length >= 8 ? nid : throw new CoreBusinessException("the document requires at least 8 chars");
        DateOfBirth = dateOfBirth;
        Origin = origin;
    }

    public bool IsUnderAge => ((new DateTime((DateTime.Now - DateOfBirth).Ticks).Year) - 1) < MinimumAge;

    public bool CanVoteBasedOnLocation => Origin.ToUpper(System.Globalization.CultureInfo.InvariantCulture) == CountryOfOrigin;

    public string Nid { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Origin { get; init; }

    public Guid TestId { get; set; }
    public virtual Test? Test { get; set; }
}

public class Test : EntityBase<Guid>
{
    public string Name { get; set; }
    public Test(string name)
    {
        Name = name;
    }
}

