using DataMkt.Domain.Entities;

namespace DataMkt.Domain.Repositories;

public interface ISucursalRepository
{
    Task<IEnumerable<Sucursal>> GetAllAsync(CancellationToken ct = default);
    Task<Sucursal?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Sucursal>  AddAsync(Sucursal sucursal, CancellationToken ct = default);
    Task RemoveAsync(Sucursal sucursal, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}