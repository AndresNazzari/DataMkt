using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataMkt.Application.Ventas.Dto;

/// <summary>
/// DTO utilizado para enviar un archivo CSV con los registros de ventas
/// que se desean importar al sistema.
/// </summary>
/// <remarks>
/// Consumido por el endpoint:
/// <c>POST /api/Ventas/importar</c>
///
/// **Ejemplo (multipart/form-data)**  
/// ```
 
///  ├─ file: ventas_julio.csv
/// ```
/// El controlador deberá procesar el stream recibido, validar su formato y
/// devolver el resultado de la importación.
/// </remarks>
public class ImportarVentasRequest
{
    /// <summary>
    /// Archivo CSV que contiene los registros de ventas.
    /// El nombre de la parte debe ser <c>file</c> en la solicitud.
    /// </summary>
    /// <example>ventas_julio.csv</example>
    [Required(ErrorMessage = "Debe adjuntar un archivo CSV.")]
    [DataType(DataType.Upload)]
    public IFormFile File { get; set; } = default!;
}