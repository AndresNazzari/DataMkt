using DataMkt.Application.Sucursal.Dto;
using DataMkt.Application.Sucursal.Services;
using DataMkt.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador encargado de consultar y administrar las sucursales del sistema.
/// </summary>
/// <remarks>
/// Rutas disponibles:  
/// • <c>GET  /api/Sucursales</c>      – Listado de sucursales.  
/// • <c>POST /api/Sucursales</c>      – Crear una sucursal.  
/// • <c>DELETE /api/Sucursales/{id}</c> – Eliminar una sucursal.  
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalService _service;

    /// <inheritdoc />
    public SucursalesController(ISucursalService service)
    {
        _service = service;
    }

    /// <summary>Obtiene todas las sucursales registradas.</summary>
    /// <remarks>
    /// Devuelve la colección completa ordenada alfabéticamente por nombre.
    /// </remarks>
    /// <returns>Colección de <see cref="SucursalDto"/>.</returns>
    /// <response code="200">Listado de sucursales obtenido correctamente.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SucursalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
    {
        var sucursales = await _service.GetSucursalesAsync();
        return Ok(sucursales);
    }

    /// <summary>Crea una nueva sucursal.</summary>
    /// <param name="dto">
    /// Objeto con el <c>Nombre</c> de la sucursal; el <c>Id</c> se generará automáticamente.
    /// </param>
    /// <returns>La sucursal creada con su <c>Id</c> asignado.</returns>
    /// <response code="201">
    /// Sucursal creada correctamente; cabecera <c>Location</c> apunta al recurso.
    /// </response>
    /// <response code="400">Datos inválidos para crear la sucursal.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SucursalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sucursal>> CreateSucursal([FromBody] SucursalDto dto)
    {
        var created = await _service.CreateSucursalAsync(dto);
        return CreatedAtAction(nameof(GetSucursales), new { id = created.Id }, created);
    }
    
    /// <summary>Elimina una sucursal existente.</summary>
    /// <param name="id">Identificador de la sucursal a eliminar.</param>
    /// <response code="204">Sucursal eliminada exitosamente.</response>
    /// <response code="404">No se encontró la sucursal especificada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSucursal(int id)
    {
        await _service.DeleteSucursalAsync(id);
        return NoContent();
    }
}