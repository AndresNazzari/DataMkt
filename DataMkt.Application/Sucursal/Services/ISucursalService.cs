using DataMkt.Application.Sucursal.Dto;

namespace DataMkt.Application.Sucursal.Services;

public interface  ISucursalService
{
    Task<IEnumerable<SucursalDto>> GetSucursalesAsync(CancellationToken ct = default);
    Task<SucursalDto> CreateSucursalAsync(SucursalDto dto, CancellationToken ct = default);
    Task DeleteSucursalAsync(int id, CancellationToken ct = default);
}