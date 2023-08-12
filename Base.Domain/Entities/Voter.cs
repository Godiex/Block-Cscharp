using Base.Domain.Exceptions;
using Domain.Entities.Base;

namespace Base.Domain.Entities;

public class Voter : EntityBase<Guid>
{
    const int MINIMUM_AGE = 18;
    const string COUNTRY_OF_ORIGIN = "COLOMBIA";

    // No empty constructor
    public Voter(string nid, DateTime dateOfBirth, string origin)
    {
        Nid = nid.Length >= 8 ? nid : throw new CoreBusinessException("the document requires at least 8 chars");
        DateOfBirth = dateOfBirth;
        Origin = origin;
    }

    // underaged ?
    public bool IsUnderAge => ((new DateTime((DateTime.Now - this.DateOfBirth).Ticks).Year) - 1) < MINIMUM_AGE;

    // location allowed to vote ?
    public bool CanVoteBasedOnLocation => this.Origin.ToUpper(System.Globalization.CultureInfo.InvariantCulture) == COUNTRY_OF_ORIGIN;


    // Private setter, if we require inmutability for the entity fields. These values we get a construction time.

    public string Nid { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Origin { get; init; }
}

