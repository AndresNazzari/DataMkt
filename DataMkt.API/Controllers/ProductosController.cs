using DataMkt.Application.Producto.Dto;
using DataMkt.Domain.Entities;
using DataMkt.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los productos registrados.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _context.Productos.ToListAsync();
        return Ok(productos);
    }

    /// <summary>
    /// Actualiza el stock de un producto por ID.
    /// </summary>
    /// <param name="dto">DTO con ID y nuevo stock</param>
    [HttpPut("stock")]
    public async Task<IActionResult> ActualizarStockPorSucursal([FromBody] ActualizarStockSucursalDto dto)
    {
        var registroStock = await _context.StocksPorSucursal
            .FirstOrDefaultAsync(s =>
                s.ProductoId == dto.ProductoId && s.SucursalId == dto.SucursalId);

        if (registroStock == null)
        {
            // Crear el registro si no existe
            registroStock = new StockPorSucursal
            {
                ProductoId = dto.ProductoId,
                SucursalId = dto.SucursalId,
                Stock = dto.Cantidad
            };
            _context.StocksPorSucursal.Add(registroStock);
        }
        else
        {
            registroStock.Stock += dto.Cantidad;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

}
