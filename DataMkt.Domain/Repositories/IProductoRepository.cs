using DataMkt.Domain.Entities;

namespace DataMkt.Domain.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct = default);
    Task<Producto> AddAsync(Producto producto, CancellationToken ct = default);
    Task<Producto?> GetByIdAsync(int id, CancellationToken ct = default);
}