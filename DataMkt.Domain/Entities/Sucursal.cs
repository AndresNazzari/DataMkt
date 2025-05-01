namespace DataMkt.Domain.Entities;

public class Sucursal
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<StockPorSucursal>? Stocks { get; set; }
}