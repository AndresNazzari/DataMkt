namespace DataMkt.Application.Ventas.Dto;



/// <summary>
/// Representa una venta registrada con datos enriquecidos del producto y sucursal.
/// </summary>
public class VentaDto
{
    /// <summary>
    /// Fecha en la que se realizó la venta.
    /// </summary>
    /// <example>2025-07-01T14:30:00</example>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Nombre del producto vendido.
    /// </summary>
    /// <example>Notebook Lenovo IdeaPad 3</example>
    public string Producto { get; set; }

    /// <summary>
    /// Nombre de la sucursal donde se registró la venta.
    /// </summary>
    /// <example>Sucursal Palermo</example>
    public string Sucursal { get; set; }

    /// <summary>
    /// Cantidad de unidades vendidas.
    /// </summary>
    /// <example>2</example>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del producto en el momento de la venta.
    /// </summary>
    /// <example>799.99</example>
    public decimal PrecioUnitario { get; set; }

    /// <summary>
    /// Total de la venta (Cantidad x PrecioUnitario).
    /// </summary>
    /// <example>1599.98</example>
    public decimal Total => Cantidad * PrecioUnitario;
}