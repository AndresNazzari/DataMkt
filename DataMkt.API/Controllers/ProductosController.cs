using DataMkt.Application.Producto.Dto;
using DataMkt.Domain.Entities;
using DataMkt.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador que gestiona los productos y el stock por sucursal.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor del controlador de productos.
    /// </summary>
    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los productos registrados en el sistema.
    /// </summary>
    /// <remarks>
    /// Este endpoint devuelve todos los productos disponibles en la base de datos.
    /// </remarks>
    /// <returns>Lista de productos</returns>
    /// <response code="200">Lista de productos obtenida correctamente.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Producto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _context.Productos.ToListAsync();
        return Ok(productos);
    }

    /// <summary>
    /// Obtiene el stock de productos por sucursal.
    /// </summary>
    /// <remarks>
    /// Devuelve una lista con el nombre del producto, la sucursal y la cantidad de stock disponible en cada una.
    /// </remarks>
    /// <returns>Listado de stock por sucursal</returns>
    /// <response code="200">Listado obtenido correctamente.</response>
    [HttpGet("con-stock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStockPorSucursal()
    {
        var stock = await _context.StocksPorSucursal
            .Include(s => s.Producto)
            .Include(s => s.Sucursal)
            .Select(s => new
            {
                Producto = s.Producto!.Nombre,
                Sucursal = s.Sucursal!.Nombre,
                Stock = s.Stock
            })
            .ToListAsync();

        return Ok(stock);
    }
    
    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    /// <param name="producto">Objeto producto a registrar.</param>
    /// <returns>Producto creado con su ID generado.</returns>
    /// <response code="201">Producto creado correctamente.</response>
    /// <response code="400">Error de validación en los datos del producto.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Producto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductos), new { id = producto.Id }, producto);
    }

    /// <summary>
    /// Actualiza el stock de un producto en una sucursal.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite incrementar o reducir el stock de un producto en una sucursal específica.
    /// Si no existe el registro de stock para esa sucursal, se crea automáticamente.
    /// </remarks>
    /// <param name="dto">DTO con el ID del producto, ID de la sucursal y cantidad a modificar.</param>
    /// <returns>Respuesta sin contenido en caso de éxito.</returns>
    /// <response code="204">Stock actualizado correctamente.</response>
    /// <response code="400">Error de validación en los datos enviados.</response>
    [HttpPut("stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ActualizarStockPorSucursal([FromBody] ActualizarStockSucursalDto dto)
    {
        var registroStock = await _context.StocksPorSucursal
            .FirstOrDefaultAsync(s =>
                s.ProductoId == dto.ProductoId && s.SucursalId == dto.SucursalId);

        if (registroStock is null)
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
