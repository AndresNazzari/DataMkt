namespace DataMkt.Domain.Entities;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int ProductoId { get; set; }
    public int SucursalId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public Producto? Producto { get; set; }
    public Sucursal? Sucursal { get; set; }
}