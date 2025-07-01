namespace DataMkt.Application.Ventas.Dto;

/// <summary>
/// Representa una venta importada desde un archivo externo (CSV, Excel, etc.).
/// </summary>
public class VentaImportadaDto
{
    /// <summary>
    /// Fecha de la venta.
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
    /// Cantidad de unidades vendidas.
    /// </summary>
    /// <example>5</example>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del producto al momento de la venta.
    /// </summary>
    /// <example>499.99</example>
    public decimal PrecioUnitario { get; set; }
}