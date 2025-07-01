using DataMkt.Application.Ventas.Dto;
using DataMkt.Domain.Entities;

namespace DataMkt.Application.Ventas.Repositories;

public interface IVentaRepository
{
    Task AddRangeAsync(IEnumerable<Venta> ventas);
    Task SaveChangesAsync();
    Task<List<VentaDto>> GetVentasAsync();
}