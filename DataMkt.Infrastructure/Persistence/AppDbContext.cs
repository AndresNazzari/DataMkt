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
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<StockPorSucursal> StocksPorSucursal => Set<StockPorSucursal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}