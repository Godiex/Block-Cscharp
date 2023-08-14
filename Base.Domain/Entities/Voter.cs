﻿using Base.Domain.Exceptions;
using Domain.Entities.Base;

namespace Base.Domain.Entities;

public class Voter : EntityBase<Guid>
{
    private const int MinimumAge = 18;
    private const string CountryOfOrigin = "COLOMBIA";

    public Voter(string nid, DateTime dateOfBirth, string origin)
    {
        Nid = nid.Length >= 8 ? nid : throw new CoreBusinessException("the document requires at least 8 chars");
        DateOfBirth = dateOfBirth;
        Origin = origin;
    }

    public bool IsUnderAge => ((new DateTime((DateTime.Now - this.DateOfBirth).Ticks).Year) - 1) < MinimumAge;

    public bool CanVoteBasedOnLocation => this.Origin.ToUpper(System.Globalization.CultureInfo.InvariantCulture) == CountryOfOrigin;

    public string Nid { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Origin { get; init; }
}

