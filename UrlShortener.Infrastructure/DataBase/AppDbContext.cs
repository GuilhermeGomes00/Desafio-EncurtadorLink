using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entity;

namespace UrlShortener.Infrastructure.DataBase;

public class AppDbContext : DbContext
{
    public DbSet<UrlItem> UrlItems { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UrlItem>(entityTypeBuilder =>
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.LongUrl).IsRequired().HasMaxLength(250);
            entityTypeBuilder.HasIndex(x => x.LongUrl).IsUnique();

            entityTypeBuilder.Property(x => x.ShortCode).IsRequired().HasMaxLength(6);
            entityTypeBuilder.HasIndex(x => x.ShortCode).IsUnique();
        });
    }
}