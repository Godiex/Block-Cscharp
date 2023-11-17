using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configuration;

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

        builder
            .HasOne(v => v.Test)
            .WithOne()
            .HasForeignKey<Voter>(v => v.TestId);
    }
    
    
}

public class TestConfig : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder
            .ToTable("Test", SchemaNames.Base);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

    }
}