using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.MSSQL.Configuration;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(b => b.Status)
            .HasConversion<int>() 
            .HasDefaultValue(BookingStatus.Pending);

        builder.Property(b => b.CreatedBy)
            .HasMaxLength(100);

        builder.Property(b => b.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(b => b.DeletedBy)
            .HasMaxLength(100);

        builder.Property(b => b.UserId)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(b => b.StartDate)
            .IsRequired();

        builder.Property(b => b.EndDate)
            .IsRequired();

        builder.HasOne(b => b.House)
            .WithMany(h => h.Bookings)
            .HasForeignKey(b => b.HouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
