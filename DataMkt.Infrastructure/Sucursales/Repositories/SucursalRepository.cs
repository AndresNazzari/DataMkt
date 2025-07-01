using DataMkt.Domain.Entities;
using DataMkt.Domain.Repositories;
using DataMkt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Sucursales.Repositories;

public class SucursalRepository : ISucursalRepository
{
    private readonly AppDbContext _ctx;
    public SucursalRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<Sucursal>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Sucursales.AsNoTracking().ToListAsync(ct);

    public Task<Sucursal?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Sucursales.FindAsync(new object[] { id }, ct).AsTask();

    public async Task<Sucursal> AddAsync(Sucursal sucursal, CancellationToken ct = default)
    {
        _ctx.Sucursales.Add(sucursal);
        await _ctx.SaveChangesAsync(ct);
        return sucursal;
    }

    public async Task RemoveAsync(Sucursal sucursal, CancellationToken ct = default)
    {
        _ctx.Sucursales.Remove(sucursal);
        await _ctx.SaveChangesAsync(ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default) =>
        _ctx.SaveChangesAsync(ct);
}