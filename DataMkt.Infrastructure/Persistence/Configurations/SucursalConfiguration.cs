using DataMkt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataMkt.Infrastructure.Persistence.Configurations;

public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> entity)
    {
        entity.HasKey(s => s.Id);

        entity.Property(s => s.Nombre)
            .IsRequired()
            .HasMaxLength(100);
    }
}