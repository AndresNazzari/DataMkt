using DataMkt.Domain.Entities;

namespace DataMkt.Domain.Repositories;

public interface IStockPorSucursalRepository
{
    Task<IEnumerable<StockPorSucursal>> GetAllWithIncludesAsync(CancellationToken ct = default);
    Task<StockPorSucursal?> FindAsync(int productoId, int sucursalId, CancellationToken ct = default);
    Task AddAsync(StockPorSucursal stock, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}