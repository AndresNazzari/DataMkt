using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly ImportarVentasService _importarVentasService;

    public VentasController(ImportarVentasService importarVentasService)
    {
        _importarVentasService = importarVentasService;
    }

    /// <summary>
    /// Obtiene todas las ventas registradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<VentaDto>>> GetVentas()
    {
        var ventas = await _importarVentasService.ObtenerVentasAsync();
        return Ok(ventas);
    }
    
    /// <summary>
    /// Importa ventas desde un archivo CSV.
    /// </summary>
    [HttpPost("importar")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ImportarVentas([FromForm] ImportarVentasRequest request)
    {
        if (request.File is null || request.File.Length == 0)
            return BadRequest("Debe subir un archivo CSV válido.");

        await using var stream = request.File.OpenReadStream();
        await _importarVentasService.ImportarAsync(stream);

        return Ok("Ventas importadas correctamente.");
    }
}