using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Repositories;
using DataMkt.Domain.Entities;
using DataMkt.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.Infrastructure.Ventas.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly AppDbContext _context;

    public VentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<Venta> ventas)
    {
        await _context.Ventas.AddRangeAsync(ventas);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<VentaDto>> GetVentasAsync()
    {
        return await _context.Ventas
            .Include(v => v.Producto)
            .Include(v => v.Sucursal)
            .Select(v => new VentaDto
            {
                Fecha = v.Fecha,
                Producto = v.Producto.Nombre,
                Sucursal = v.Sucursal.Nombre,
                Cantidad = v.Cantidad,
                PrecioUnitario = v.PrecioUnitario
            })
            .ToListAsync();
    }
}
