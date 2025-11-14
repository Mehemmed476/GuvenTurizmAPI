using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.MSSQL.Configuration;

public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.ToTable("Houses");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id)
            .IsRequired();

        builder.Property(h => h.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(h => h.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(h => h.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Description)
            .HasMaxLength(2000);

        builder.Property(h => h.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(h => h.NumberOfRooms)
            .HasDefaultValue((byte)0);

        builder.Property(h => h.NumberOfBeds)
            .HasDefaultValue((byte)0);

        builder.Property(h => h.NumberOfFloors)
            .HasDefaultValue((byte)0);

        builder.Property(h => h.Field)
            .HasDefaultValue(0);

        builder.Property(h => h.Address)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(h => h.City)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(h => h.GoogleMapsCode)
            .HasMaxLength(500);

        builder.Property(h => h.CoverImage)
            .HasMaxLength(500);

        builder.Property(h => h.CreatedBy)
            .HasMaxLength(100);

        builder.Property(h => h.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(h => h.DeletedBy)
            .HasMaxLength(100);

        builder.HasOne(h => h.Category)
            .WithMany(c => c.Houses)
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(h => h.Images)
            .WithOne(f => f.House)
            .HasForeignKey(f => f.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Bookings)
            .WithOne(b => b.House)
            .HasForeignKey(b => b.HouseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(h => h.HouseHouseAdvantageRels)
            .WithOne(h => h.House)
            .HasForeignKey(h => h.HouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
