namespace DataMkt.Application.Ventas.Dto;

/// <summary>
/// DTO que representa una venta leída de un archivo externo
/// (CSV, Excel u otro formato) antes de ser persistida en el sistema.
/// </summary>
/// <remarks>
/// Normalmente se genera durante la importación de archivos en el
/// endpoint <c>POST /api/Ventas/importar</c>.
///
/// Ejemplo de payload una vez parseado:
/// <code>
/// {
///   "fecha": "2025-07-01T10:00:00",
///   "productoId": 1,
///   "sucursalId": 3,
///   "cantidad": 5,
///   "precioUnitario": 499.99
/// }
/// </code>
/// </remarks>
public class VentaImportadaDto
{
    /// <summary>
    /// Fecha y hora de la venta (formato ISO-8601).
    /// </summary>
    /// <example>2025-07-01T10:00:00</example>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Identificador del producto vendido.
    /// </summary>
    /// <example>1</example>
    public int ProductoId { get; set; }

    /// <summary>
    /// Identificador de la sucursal donde se realizó la venta.
    /// </summary>
    /// <example>3</example>
    public int SucursalId { get; set; }

    /// <summary>
    /// Número de unidades vendidas.
    /// </summary>
    /// <example>5</example>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del producto en el momento de la venta.
    /// </summary>
    /// <example>499.99</example>
    public decimal PrecioUnitario { get; set; }
}