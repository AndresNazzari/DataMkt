using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMkt.Infrastructure.Persistence.Configurations;

public class StockPorSucursalConfiguration : IEntityTypeConfiguration<StockPorSucursal>
{
    public void Configure(EntityTypeBuilder<StockPorSucursal> entity)
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
    }
}