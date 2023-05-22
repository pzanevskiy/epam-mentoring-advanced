using ApiDemo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Data;

public class DemoContext : DbContext
{
    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Category)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Item> Items { get; set; }

    public DbSet<Category> Categories { get; set; }
}