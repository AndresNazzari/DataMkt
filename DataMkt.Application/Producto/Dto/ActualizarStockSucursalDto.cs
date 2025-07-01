namespace DataMkt.Application.Producto.Dto;


/// <summary>
/// DTO utilizado para modificar la cantidad de stock de un producto en una
/// sucursal concreta. La <see cref="Cantidad"/> puede ser positiva (entradas) o
/// negativa (salidas).
/// </summary>
/// <remarks>
/// Se consume desde el endpoint:
/// <c>PUT /api/Productos/{productoId}/sucursales/{sucursalId}/stock</c>.
///
/// Ejemplo de payload:
/// <code>
/// {
///   "productoId": 1,
///   "sucursalId":  2,
///   "cantidad":   10
/// }
/// </code>
/// </remarks>
public class ActualizarStockSucursalDto
{
    /// <summary>
    /// Identificador del producto cuyo stock va a modificarse.
    /// </summary>
    /// <example>1</example>
    public int ProductoId { get; set; }

    /// <summary>
    /// Identificador de la sucursal sobre la que se realiza la operación.
    /// </summary>
    /// <example>2</example>
    public int SucursalId { get; set; }

    /// <summary>
    /// Cantidad a ajustar:
    /// <list type="bullet">
    ///   <item>Positiva → incrementa el stock.</item>
    ///   <item>Negativa → descuenta stock.</item>
    /// </list>
    /// </summary>
    /// <example>10</example>
    public int Cantidad { get; set; }
}