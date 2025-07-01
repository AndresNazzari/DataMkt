using Microsoft.AspNetCore.Mvc;

namespace DataMkt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public ReportesController(IWebHostEnvironment env)
    {
        _env = env;
    }

    /// <summary>
    /// Devuelve el último reporte de ventas en formato Excel.
    /// </summary>
    [HttpGet("ventas")]
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