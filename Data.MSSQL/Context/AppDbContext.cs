using System.Reflection;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.MSSQL.Context;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<House> Houses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<HouseFile> Files { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<HouseAdvantage> HouseAdvantages { get; set; } 
    public DbSet<HouseHouseAdvantageRel> HouseHouseAdvantageRels { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedData(builder);
    }
    
    private void SeedData(ModelBuilder builder)
    {
        var cat1 = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Deniz Manzaralı Villalar",
            Description = "Denize sıfır, özel havuzlu villalar."
        };

        var cat2 = new Category
        {
            Id = Guid.NewGuid(),
            Title = "Şehir Daireleri",
            Description = "Merkezi konumda modern daireler."
        };

        var house1 = new House
        {
            Id = Guid.NewGuid(),
            Title = "Kaş’ta Deniz Manzaralı Villa",
            Description = "3 katlı, 4 odalı, özel havuzlu mükemmel villa.",
            Price = 1200.00m,
            NumberOfRooms = 4,
            NumberOfBeds = 6,
            NumberOfFloors = 3,
            Field = 350,
            Address = "Kaş, Antalya",
            City = "Antalya",
            GoogleMapsCode = "https://maps.google.com/...",
            CoverImage = "villa1.jpg",
            CategoryId = cat1.Id,
        };

        var house2 = new House
        {
            Id = Guid.NewGuid(),
            Title = "İstanbul Merkezde Modern Daire",
            Description = "Metroya yakın, 2 odalı şık daire.",
            Price = 850.00m,
            NumberOfRooms = 2,
            NumberOfBeds = 2,
            NumberOfFloors = 1,
            Field = 90,
            Address = "Şişli, İstanbul",
            City = "İstanbul",
            GoogleMapsCode = "https://maps.google.com/...",
            CoverImage = "daire1.jpg",
            CategoryId = cat2.Id,
        };

        var file1 = new HouseFile
        {
            Id = Guid.NewGuid(),
            HouseId = house1.Id,
            Image = "villa1_1.jpg"
        };

        var file2 = new HouseFile
        {
            Id = Guid.NewGuid(),
            HouseId = house1.Id,
            Image = "villa1_2.jpg"
        };

        var file3 = new HouseFile
        {
            Id = Guid.NewGuid(),
            HouseId = house2.Id,
            Image = "daire1_1.jpg"
        };

        builder.Entity<Category>().HasData(cat1, cat2);
        builder.Entity<House>().HasData(house1, house2);
        builder.Entity<HouseFile>().HasData(file1, file2, file3);
    }
}

