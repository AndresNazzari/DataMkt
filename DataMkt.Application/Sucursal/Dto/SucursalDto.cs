namespace DataMkt.Application.Sucursal.Dto;

/// <summary>
/// DTO expuesto por la capa de Aplicación que representa
/// una sucursal (Id y nombre) sin exponer la entidad de dominio.
/// </summary>
/// <remarks>
/// Devuelto, por ejemplo, en:
/// <c>GET /api/Sucursales</c>
/// <c>GET /api/Sucursales/{id}</c>
///
/// Ejemplo de payload:
/// <code>
/// {
///   "id": 3,
///   "nombre": "Barcelona Eixample"
/// }
/// </code>
/// </remarks>
public class SucursalDto
{
    /// <summary>
    /// Identificador único de la sucursal.
    /// </summary>
    /// <example>3</example>
    public int Id { get; init; }

    /// <summary>
    /// Nombre descriptivo de la sucursal.
    /// </summary>
    /// <example>Barcelona Eixample</example>
    public string Nombre { get; init; } = string.Empty;
}