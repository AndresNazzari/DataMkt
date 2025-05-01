namespace DataMkt.Domain.Entities;

public class Venta
{
    public int Id { get; set; }
    public string Producto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; }
}