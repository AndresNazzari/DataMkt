namespace DataMkt.Application.Ventas.Dto;

/// <summary>
/// DTO devuelto por la capa de Aplicación que representa una venta ya
/// “enriquecida” con la descripción del producto y la sucursal.
/// </summary>
/// <remarks>
/// Aparece como respuesta en:
/// <c>GET /api/Ventas</c>
/// <c>GET /api/Ventas/{id}</c>
///
/// Ejemplo de payload:
/// <code>
/// {
///   "fecha": "2025-07-01T14:30:00",
///   "producto": "Notebook Lenovo IdeaPad 3",
///   "sucursal": "Sucursal Palermo",
///   "cantidad": 2,
///   "precioUnitario": 799.99,
///   "total": 1599.98
/// }
/// </code>
/// </remarks>
public class VentaDto
{
    /// <summary>
    /// Fecha y hora en la que se registró la venta (en UTC o zona configurada
    /// por la API).
    /// </summary>
    /// <example>2025-07-01T14:30:00</example>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Nombre descriptivo del producto vendido.
    /// </summary>
    /// <example>Notebook Lenovo IdeaPad 3</example>
    public string Producto { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de la sucursal donde se efectuó la venta.
    /// </summary>
    /// <example>Sucursal Palermo</example>
    public string Sucursal { get; set; } = string.Empty;

    /// <summary>
    /// Número de unidades vendidas.
    /// </summary>
    /// <example>2</example>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio de una unidad del producto en el momento de la venta.
    /// </summary>
    /// <example>799.99</example>
    public decimal PrecioUnitario { get; set; }

    /// <summary>
    /// Importe total de la venta (<c>Cantidad × PrecioUnitario</c>).
    /// Calculado a partir de las otras propiedades.
    /// </summary>
    /// <example>1599.98</example>
    public decimal Total => Cantidad * PrecioUnitario;
}