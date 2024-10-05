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
            .Property(nameof(DbCall.Status))
            .HasConversion<string>();

        modelBuilder.Entity<DbCall>()
            .Property(nameof(DbCall.People))
            .HasConversion(splitStringConverter);

        modelBuilder.Entity<DbCall>()
            .Property(nameof(DbCall.Locations))
            .HasConversion(splitStringConverter);
    }
}