using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

/// <summary>
/// Controlador responsable de gestionar los reportes generados por el sistema.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    
    /// <summary>
    /// Constructor que inyecta el entorno web actual.
    /// </summary>
    public ReportesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    /// <summary>
    /// Descarga el reporte de ventas más reciente en formato Excel.
    /// </summary>
    /// <remarks>
    /// Busca el archivo `reporte_ventas.xlsx` dentro de la carpeta `Reportes` del proyecto.  
    /// Este archivo es generado automáticamente luego de importar las ventas.  
    /// Asegúrese de haber importado ventas antes de intentar descargar el reporte.
    /// </remarks>
    /// <returns>Archivo Excel con el reporte de ventas.</returns>
    /// <response code="200">Reporte descargado exitosamente.</response>
    /// <response code="404">No se encontró el archivo de reporte.</response>
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