using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador que permite consultar el historial de ventas e importar nuevas
/// ventas desde un archivo CSV.
/// </summary>
/// <remarks>
/// Rutas disponibles:  
/// • <c>GET  /api/Ventas</c>             – Listado de ventas.  
/// • <c>POST /api/Ventas/importar</c>    – Importar ventas vía CSV.  
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VentasController : ControllerBase
{
    private readonly ImportarVentasService _importarVentasService;

    /// <inheritdoc />
    public VentasController(ImportarVentasService importarVentasService)
    {
        _importarVentasService = importarVentasService;
    }

    /// <summary>Obtiene todas las ventas registradas.</summary>
    /// <remarks>
    /// Devuelve la colección completa ordenada por fecha descendente.
    /// </remarks>
    /// <returns>Colección de <see cref="VentaDto"/>.</returns>
    /// <response code="200">Lista de ventas obtenida correctamente.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VentaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<VentaDto>>> GetVentas()
    {
        var ventas = await _importarVentasService.ObtenerVentasAsync();
        return Ok(ventas);
    }
    
    /// <summary>Importa ventas desde un archivo CSV.</summary>
    /// <remarks>
    /// * El archivo debe ser **multipart/form-data**, con la parte llamada
    ///   <c>file</c>. <br/>
    /// * Columnas requeridas: <c>Fecha</c>, <c>ProductoId</c>, <c>SucursalId</c>,
    ///   <c>Cantidad</c>, <c>PrecioUnitario</c>. <br/>
    /// * Al finalizar se lanza la generación del reporte de ventas.
    ///
    /// **Ejemplo cURL**  
    /// <c>
    /// curl -F "file=@ventas_julio.csv" https://tu-api.com/api/Ventas/importar
    /// </c>
    /// </remarks>
    /// <param name="request">
    /// Form-data con el archivo CSV a procesar.
    /// </param>
    /// <returns>Mensaje de confirmación.</returns>
    /// <response code="200">Ventas importadas correctamente.</response>
    /// <response code="400">Archivo faltante o con formato incorrecto.</response>
    [HttpPost("importar")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportarVentas([FromForm] ImportarVentasRequest request)
    {
        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("Debe subir un archivo CSV válido.");
        }
        
        var extension = Path.GetExtension(request.File.FileName)
            .TrimStart('.')
            .ToLowerInvariant();
        
        await using var stream = request.File.OpenReadStream();
        await _importarVentasService.ImportarAsync(stream, extension);

        return Ok($"Ventas importadas correctamente desde {extension.ToUpper()}.");
    }
}