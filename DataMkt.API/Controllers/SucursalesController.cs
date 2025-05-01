using DataMkt.Domain.Entities;
using DataMkt.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataMkt.API.Controllers;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
    {
        var sucursales = await _context.Sucursales.ToListAsync();
        return Ok(sucursales);
    }

    /// <summary>
    /// Crea una nueva sucursal.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Sucursal>> CreateSucursal([FromBody] Sucursal nuevaSucursal)
    {
        _context.Sucursales.Add(nuevaSucursal);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSucursales), new { id = nuevaSucursal.Id }, nuevaSucursal);
    }
}