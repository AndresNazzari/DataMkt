namespace DataMkt.Application.Ventas.Dto;

public class VentaDto
{
    public DateTime Fecha { get; set; }
    public string Producto { get; set; }
    public string Sucursal { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total => Cantidad * PrecioUnitario;
}