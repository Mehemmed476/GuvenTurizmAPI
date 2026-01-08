using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.MSSQL.Configuration;

public class TourPackageConfiguration : IEntityTypeConfiguration<TourPackage>
{
    public void Configure(EntityTypeBuilder<TourPackage> builder)
    {
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");

        builder.HasMany(p => p.Inclusions)
            .WithOne(i => i.TourPackage)
            .HasForeignKey(i => i.TourPackageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}