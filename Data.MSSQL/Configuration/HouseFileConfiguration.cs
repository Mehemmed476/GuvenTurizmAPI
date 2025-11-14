using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.MSSQL.Configuration;

public class HouseFileConfiguration : IEntityTypeConfiguration<HouseFile>
{
    public void Configure(EntityTypeBuilder<HouseFile> builder)
    {
        builder.ToTable("HouseFiles");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(f => f.Image)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(f => f.House)
            .WithMany(h => h.Images)
            .HasForeignKey(f => f.HouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
