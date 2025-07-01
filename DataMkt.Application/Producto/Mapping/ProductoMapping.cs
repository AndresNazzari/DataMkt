using DataMkt.Application.Producto.Dto;

namespace DataMkt.Application.Producto.Mapping;

/// <summary>
/// Métodos de extensión para mapear entre Entidad y DTO.
/// </summary>
public static class ProductoMapping
{
    public static ProductoDto ToDto(this Domain.Entities.Producto entity) => new()
    {
        Id = entity.Id,
        Nombre = entity.Nombre,
        Precio = entity.Precio
    };

    public static Domain.Entities.Producto ToEntity(this ProductoDto dto) => new()
    {
        Id = dto.Id,
        Nombre = dto.Nombre,
        Precio = dto.Precio
    };
}
