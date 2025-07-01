namespace DataMkt.Application.Producto.Dto;

/// <summary>
///  DTO utilizado por la capa de Aplicación para solicitar
///  un cambio de stock sobre un producto existente.
/// </summary>
/// <remarks>
///  Se consume desde el endpoint <c>PUT /api/Productos/{id}/stock</c>.
///  Ejemplo de payload:
///
///  <code>
///  {
///     "productoId": 42,
///     "nuevoStock": 150
///  }
///  </code>
/// </remarks>
public class ActualizarStockDto
{
    /// <summary>
    /// Identificador único del producto cuyo stock será actualizado.
    /// </summary>
    public int ProductoId { get; set; }

    /// <summary>
    /// Cantidad final de stock que tendrá el producto
    /// tras la operación. Debe ser un entero no negativo.
    /// </summary>
    public int NuevoStock { get; set; }
}
