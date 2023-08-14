using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Context.Configuration;

public class GymConfig : IEntityTypeConfiguration<Voter>
{

    public void Configure(EntityTypeBuilder<Voter> builder)
    {
        builder
            .ToTable("Voter", SchemaNames.Base);

        builder
            .Property(x => x.Origin)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Nid)
            .HasMaxLength(50);
    }
}
