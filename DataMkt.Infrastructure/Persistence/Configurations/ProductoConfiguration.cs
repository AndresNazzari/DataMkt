using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMkt.Infrastructure.Persistence.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> entity)
    {
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(p => p.Precio)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}