using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataMkt.Application.Ventas.Dto;

/// <summary>
/// DTO que representa un archivo de ventas a importar.
/// </summary>
public class ImportarVentasRequest
{
    /// <summary>
    /// Archivo CSV que contiene los registros de ventas.
    /// </summary>
    /// <example>ventas_julio.csv</example>
    public IFormFile File { get; set; } = default!;
}