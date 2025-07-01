using DataMkt.Application.Sucursal.Dto;
using DataMkt.Application.Sucursal.Services;
using DataMkt.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador que permite consultar y administrar las sucursales del sistema.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalService _service;

    public SucursalesController(ISucursalService service)
    {
        _service = service;
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
        var sucursales = await _service.GetSucursalesAsync();
        return Ok(sucursales);
    }

    /// <summary>
    /// Crea una nueva sucursal.
    /// </summary>
    /// <param name="dto">Sucursal a crear.</param>
    /// <returns>La sucursal creada.</returns>
    /// <response code="201">Sucursal creada exitosamente.</response>
    /// <response code="400">Datos inválidos para crear la sucursal.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sucursal>> CreateSucursal([FromBody] SucursalDto dto)
    {
        var created = await _service.CreateSucursalAsync(dto);
        return CreatedAtAction(nameof(GetSucursales), new { id = created.Id }, created);
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
        await _service.DeleteSucursalAsync(id);
        return NoContent();
    }
}