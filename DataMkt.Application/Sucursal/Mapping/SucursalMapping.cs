using DataMkt.Application.Sucursal.Dto;

namespace DataMkt.Application.Sucursal.Mapping;

public static class SucursalMapping
{
    public static SucursalDto ToDto(this Domain.Entities.Sucursal entity) => new()
    {
        Id = entity.Id, 
        Nombre = entity.Nombre
    };
    
    public static Domain.Entities.Sucursal ToEntity(this SucursalDto dto) => new()
    {
        Id = dto.Id,
        Nombre = dto.Nombre
    };
}