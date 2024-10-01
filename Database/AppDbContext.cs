using Database.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DbCategory> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var splitStringConverter = new ValueConverter<HashSet<string>, string>(
            v => string.Join(";", v),
            v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
        );
        modelBuilder.Entity<DbCategory>()
            .Property(nameof(DbCategory.Points))
            .HasConversion(splitStringConverter);

        modelBuilder.Entity<DbCategory>().HasData(
            new DbCategory
            {
                Id = 1,
                Title = "Visa and Passport Services",
                Points = ["Visa Application", "Passport Application", "Visa Extension"]
            },
            new DbCategory
            {
                Id = 2,
                Title = "Diplomatic Inquiries",
                Points = ["Diplomatic Passport", "Diplomatic Visa", "Diplomatic Note"]
            },
            new DbCategory
            {
                Id = 3,
                Title = "Travel Advisories",
                Points = ["Travel Warning", "Travel Alert", "Travel Advisory"]
            },
            new DbCategory
            {
                Id = 4,
                Title = "Consular Assistance",
                Points = ["Consular Report of Birth Abroad", "Notarial Services", "Emergency Assistance"]
            },
            new DbCategory
            {
                Id = 5,
                Title = "Trade and Economic Cooperation",
                Points = ["Trade Agreement", "Trade Mission", "Trade Show"]
            }
        );
    }
}