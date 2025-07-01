namespace DataMkt.Application.Producto.Dto;

/// <summary>
/// DTO que representa la cantidad disponible de un producto
/// en una sucursal concreta.
/// </summary>
/// <remarks>
/// Devuelto, por ejemplo, en:
/// <c>GET /api/Stock/por-sucursal</c>
///
/// Ejemplo de payload:
/// <code>
/// {
///   "producto": "Camiseta básica",
///   "sucursal": "Madrid Centro",
///   "stock": 120
/// }
/// </code>
/// </remarks>
public record StockPorSucursalDto
{
    /// <summary>
    /// Nombre o referencia del producto.
    /// </summary>
    /// <example>Camiseta básica</example>
    public string Producto { get; init; } = string.Empty;

    /// <summary>
    /// Nombre descriptivo de la sucursal.
    /// </summary>
    /// <example>Madrid Centro</example>
    public string Sucursal { get; init; } = string.Empty;

    /// <summary>
    /// Cantidad disponible del producto en la sucursal indicada.
    /// </summary>
    /// <example>120</example>
    public int Stock { get; init; }
};