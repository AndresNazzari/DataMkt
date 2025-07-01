using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador responsable de gestionar los reportes generados por el sistema.
/// </summary>
/// <remarks>
/// Rutas disponibles:  
/// • <c>GET /api/Reportes/ventas</c> – Descarga el último reporte de ventas en Excel.
///
/// Todos los reportes se almacenan en la carpeta <c>Reportes/</c> del
/// contenido raíz de la aplicación.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
public class ReportesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    
    /// <inheritdoc />
    public ReportesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    /// <summary>
    /// Descarga el reporte de ventas más reciente en formato Excel.
    /// </summary>
    /// <remarks>
    /// * Busca archivos con patrón <c>Reporte_Ventas_*.xlsx</c> en
    ///   <c>&lt;content-root&gt;/Reportes</c>. <br/>
    /// * Devuelve el archivo con fecha de creación más reciente. <br/>
    /// * Es necesario haber importado ventas previamente para que exista el
    ///   reporte.
    ///
    /// **Ejemplo de uso (cURL)**  
    /// <c>curl -L -o reporte.xlsx https://tu-api.com/api/Reportes/ventas</c>
    /// </remarks>
    /// <returns>
    /// Un archivo Excel (<c>.xlsx</c>) con el reporte de ventas más reciente.
    /// </returns>
    /// <response code="200">
    /// El archivo se devuelve correctamente con cabecera
    /// <c>Content-Disposition: attachment; filename="Reporte_Ventas_YYYYMMDD.xlsx"</c>.
    /// </response>
    /// <response code="404">
    /// Se devuelve si no existe la carpeta de reportes o aún no se generó
    /// ningún archivo.
    /// </response>
    [HttpGet("ventas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DescargarUltimoReporte()
    {
        var reportesPath = Path.Combine(_env.ContentRootPath, "Reportes");

        if (!Directory.Exists(reportesPath))
        {
            return NotFound("No se encontró la carpeta de reportes.");
        }

        var ultimoReporte = Directory.GetFiles(reportesPath, "Reporte_Ventas_*.xlsx")
            .Select(f => new FileInfo(f))
            .OrderByDescending(f => f.CreationTime)
            .FirstOrDefault();

        if (ultimoReporte == null)
        {
            return NotFound("No se encontró ningún reporte generado.");
        }

        var bytes = System.IO.File.ReadAllBytes(ultimoReporte.FullName);
        return File(bytes, 
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
            ultimoReporte.Name);
    }
}