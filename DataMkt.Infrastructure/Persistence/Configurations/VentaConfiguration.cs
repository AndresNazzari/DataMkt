using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMkt.Infrastructure.Persistence.Configurations;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> entity)
    {
        entity.HasKey(v => v.Id);

        entity.Property(v => v.Fecha).IsRequired();
        entity.Property(v => v.Cantidad).IsRequired();
        entity.Property(v => v.PrecioUnitario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        entity.HasOne(v => v.Producto)
            .WithMany()
            .HasForeignKey(v => v.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(v => v.Sucursal)
            .WithMany()
            .HasForeignKey(v => v.SucursalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}