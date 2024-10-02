using Database.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DbTopic> Topics { get; set; }
    public DbSet<DbCall> Calls { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var splitStringConverter = new ValueConverter<HashSet<string>, string>(
            v => string.Join(";", v),
            v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
        );
        modelBuilder.Entity<DbTopic>()
            .Property(nameof(DbTopic.Points))
            .HasConversion(splitStringConverter);
        modelBuilder.Entity<DbCall>()
            .HasMany(c => c.Topics)
            .WithOne()
            .HasForeignKey(c => c.CallId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DbCall>()
            .Property(nameof(DbCall.People))
            .HasConversion(splitStringConverter);
        modelBuilder.Entity<DbCall>()
            .Property(nameof(DbCall.Locations))
            .HasConversion(splitStringConverter);


        //modelBuilder.Entity<DbTopic>().HasData(
        //    new DbTopic
        //    {
        //        Id = 1,
        //        Title = "Visa and Passport Services",
        //        Points = ["Visa Application", "Passport Application", "Visa Extension"]
        //    },
        //    new DbTopic
        //    {
        //        Id = 2,
        //        Title = "Diplomatic Inquiries",
        //        Points = ["Diplomatic Passport", "Diplomatic Visa", "Diplomatic Note"]
        //    },
        //    new DbTopic
        //    {
        //        Id = 3,
        //        Title = "Travel Advisories",
        //        Points = ["Travel Warning", "Travel Alert", "Travel Advisory"]
        //    },
        //    new DbTopic
        //    {
        //        Id = 4,
        //        Title = "Consular Assistance",
        //        Points = ["Consular Report of Birth Abroad", "Notarial Services", "Emergency Assistance"]
        //    },
        //    new DbTopic
        //    {
        //        Id = 5,
        //        Title = "Trade and Economic Cooperation",
        //        Points = ["Trade Agreement", "Trade Mission", "Trade Show"]
        //    }
        //);
    }
}