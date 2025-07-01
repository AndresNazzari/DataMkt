using DataMkt.Domain.Entities;
using DataMkt.Domain.Repositories;
using DataMkt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Stocks.Repositories;

public class StockPorSucursalRepository : IStockPorSucursalRepository
{
    private readonly AppDbContext _ctx;

    public StockPorSucursalRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<StockPorSucursal>> GetAllWithIncludesAsync(CancellationToken ct = default)
        => await _ctx.StocksPorSucursal
            .Include(s => s.Producto)
            .Include(s => s.Sucursal)
            .AsNoTracking()
            .ToListAsync(ct);

    public Task<StockPorSucursal?> FindAsync(int productoId, int sucursalId, CancellationToken ct = default)
        => _ctx.StocksPorSucursal.FirstOrDefaultAsync(s => s.ProductoId == productoId && s.SucursalId == sucursalId, ct);

    public async Task AddAsync(StockPorSucursal stock, CancellationToken ct = default)
    {
        _ctx.StocksPorSucursal.Add(stock);
        await _ctx.SaveChangesAsync(ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);
}