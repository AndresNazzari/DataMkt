using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Venta> Ventas => Set<Venta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Producto).IsRequired().HasMaxLength(200);
        });
    }
}