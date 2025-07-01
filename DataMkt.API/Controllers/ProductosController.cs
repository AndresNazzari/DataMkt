using DataMkt.Application.Producto.Dto;
using DataMkt.Application.Producto.Services;
using DataMkt.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador que gestiona los productos y el stock por sucursal.
/// </summary>
/// <remarks>
/// Todas las respuestas se devuelven en <c>application/json</c>.
///
/// Rutas básicas:
/// * <c>GET    /api/Productos</c>               – Lista de productos.
/// * <c>GET    /api/Productos/con-stock</c>     – Stock por sucursal.
/// * <c>POST   /api/Productos</c>               – Crear producto.
/// * <c>PUT    /api/Productos/stock</c>         – Actualizar stock en sucursal.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    /// <inheritdoc />
    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los productos registrados en el sistema.
    /// </summary>
    /// <remarks>
    /// Devuelve la colección completa ordenada alfabéticamente por nombre.
    /// </remarks>
    /// <returns>Colección de <see cref="ProductoDto"/>.</returns>
    /// <response code="200">Lista de productos obtenida correctamente.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _service.GetProductosAsync();
        return Ok(productos);
    }

    /// <summary>
    /// Obtiene el stock disponible de cada producto por sucursal.
    /// </summary>
    /// <remarks>
    /// Cada elemento incluye el nombre del producto, la sucursal y la cantidad
    /// disponible. Útil para reportes de inventario.
    /// </remarks>
    /// <returns>Colección de <see cref="StockPorSucursalDto"/>.</returns>
    /// <response code="200">Listado obtenido correctamente.</response>
    [HttpGet("con-stock")]
    [ProducesResponseType(typeof(IEnumerable<StockPorSucursalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStockPorSucursal()
    {
        var stock = await _service.GetStockPorSucursalAsync();
        return Ok(stock);
    }
    
    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    /// <param name="producto">
    /// Payload con nombre y precio; el <c>Id</c> se genera de forma automática.
    /// </param>
    /// <returns>Producto creado con su <c>Id</c> asignado.</returns>
    /// <response code="201">
    /// Producto creado correctamente; cabecera <c>Location</c> apunta al recurso.
    /// </response>
    /// <response code="400">Error de validación en los datos enviados.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> CreateProducto([FromBody] ProductoDto producto)
    {
        var created = await _service.CreateProductoAsync(producto);
        return CreatedAtAction(nameof(GetProductos), new { id = created.Id }, created);
    }

    /// <summary>
    /// Actualiza (incrementa o reduce) el stock de un producto en una sucursal.
    /// </summary>
    /// <remarks>
    /// * Si el registro de stock no existe, se crea con la cantidad indicada. <br/>
    /// * La <c>Cantidad</c> puede ser negativa para descontar. <br/>
    /// * El campo es rechazado si el resultado final sería negativo.
    /// </remarks>
    /// <param name="dto">
    /// DTO con <c>ProductoId</c>, <c>SucursalId</c> y <c>Cantidad</c> a ajustar.
    /// </param>
    /// <response code="204">Stock actualizado o insertado correctamente.</response>
    /// <response code="400">Datos inválidos (por ejemplo, stock negativo).</response>
    /// <response code="404">Producto o sucursal no encontrados.</response>
    [HttpPut("stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActualizarStockPorSucursal([FromBody] ActualizarStockSucursalDto dto)
    {
        await _service.ActualizarStockAsync(dto);
        return NoContent();
    }
}