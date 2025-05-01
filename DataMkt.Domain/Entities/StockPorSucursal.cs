namespace DataMkt.Domain.Entities;

public class StockPorSucursal
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    public int SucursalId { get; set; }
    public Sucursal? Sucursal { get; set; }

    public int Stock { get; set; }
}