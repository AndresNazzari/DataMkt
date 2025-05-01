using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<StockPorSucursal> StocksPorSucursal => Set<StockPorSucursal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
            entity.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.Stock)
                .IsRequired();
            
            entity.Property(p => p.Precio)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        
        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Nombre).IsRequired().HasMaxLength(100);
        });
        
        modelBuilder.Entity<StockPorSucursal>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.ProductoId).IsRequired();
            entity.Property(s => s.SucursalId).IsRequired();
            entity.Property(s => s.Stock).IsRequired();

            entity.HasOne(s => s.Producto)
                .WithMany()
                .HasForeignKey(s => s.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Sucursal)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.SucursalId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}