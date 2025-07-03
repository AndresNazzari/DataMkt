using System.ComponentModel.DataAnnotations;

namespace DataMkt.Application.Producto.Dto;


/// <summary>
/// DTO expuesto por la capa de Aplicación que representa un producto
/// (Id, nombre y precio) sin revelar la entidad de dominio.
/// </summary>
/// <remarks>
/// Se devuelve, por ejemplo, en:
/// <c>GET /api/Productos</c>
/// <c>GET /api/Productos/{id}</c>
///
/// Ejemplo de payload:
/// <code>
/// {
///   "id": 5,
///   "nombre": "Camiseta básica",
///   "precio": 19.99
/// }
/// </code>
/// </remarks>
public record ProductoDto
{
    /// <summary>Identificador único del producto.</summary>
    /// <example>5</example>
    [Required]
    public int Id { get; init; }

    /// <summary>Nombre descriptivo del producto.</summary>
    /// <example>Camiseta básica</example>
    [Required]
    public string Nombre { get; init; } = string.Empty;

    /// <summary>Precio de venta al público (moneda por defecto del sistema).</summary>
    /// <example>19.99</example>
    [Required]
    public decimal Precio { get; init; }
};