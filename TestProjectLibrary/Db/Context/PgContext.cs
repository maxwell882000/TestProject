using Microsoft.EntityFrameworkCore;
using TestProjectLibrary.Db.Entities;

namespace TestProjectLibrary.Db.Context;

public class PgContext : DbContext
{
    public DbSet<Point> Points { get; set; }

    public DbSet<Route> Routes { get; set; }

    public PgContext(DbContextOptions<PgContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Route>()
            .HasIndex(e => e.Hash);

        modelBuilder.Entity<Route>()
            .HasIndex(e => new { e.Destination, e.Origin });

        modelBuilder.Entity<Route>()
            .HasIndex(e => e.TimeLimit);
    }
}