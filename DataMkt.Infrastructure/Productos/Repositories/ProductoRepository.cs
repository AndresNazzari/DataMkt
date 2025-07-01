using DataMkt.Domain.Entities;
using DataMkt.Domain.Repositories;
using DataMkt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Productos.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _ctx;

    public ProductoRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Productos.AsNoTracking().ToListAsync(ct);

    public async Task<Producto> AddAsync(Producto producto, CancellationToken ct = default)
    {
        _ctx.Productos.Add(producto);
        await _ctx.SaveChangesAsync(ct);
        return producto;
    }

    public Task<Producto?> GetByIdAsync(int id, CancellationToken ct = default)
        => _ctx.Productos.FirstOrDefaultAsync(p => p.Id == id, ct);
}
