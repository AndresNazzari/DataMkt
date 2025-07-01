using DataMkt.Domain.Entities;
using DataMkt.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador que permite consultar y administrar las sucursales del sistema.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SucursalesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SucursalesController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las sucursales registradas.
    /// </summary>
    /// <returns>Lista de sucursales.</returns>
    /// <response code="200">Listado de sucursales obtenido correctamente.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
    {
        var sucursales = await _context.Sucursales.ToListAsync();
        return Ok(sucursales);
    }

    /// <summary>
    /// Crea una nueva sucursal.
    /// </summary>
    /// <param name="sucursal">Sucursal a crear.</param>
    /// <param name="nuevaSucursal"></param>
    /// <returns>La sucursal creada.</returns>
    /// <response code="201">Sucursal creada exitosamente.</response>
    /// <response code="400">Datos inválidos para crear la sucursal.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sucursal>> CreateSucursal([FromBody] Sucursal sucursal)
    {
        _context.Sucursales.Add(sucursal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSucursales), new { id = sucursal.Id }, sucursal);
    }
    
    /// <summary>
    /// Elimina una sucursal por su ID.
    /// </summary>
    /// <param name="id">ID de la sucursal a eliminar.</param>
    /// <response code="204">Sucursal eliminada exitosamente.</response>
    /// <response code="404">No se encontró la sucursal a eliminar.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSucursal(int id)
    {
        var sucursal = await _context.Sucursales.FindAsync(id);
        if (sucursal == null)
            return NotFound("Sucursal no encontrada.");

        _context.Sucursales.Remove(sucursal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}