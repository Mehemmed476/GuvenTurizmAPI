using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.MSSQL.Configuration;

public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Location).IsRequired().HasMaxLength(100);

        builder.HasMany(t => t.TourFiles)
            .WithOne(f => f.Tour)
            .HasForeignKey(f => f.TourId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TourPackages)
            .WithOne(p => p.Tour)
            .HasForeignKey(p => p.TourId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}