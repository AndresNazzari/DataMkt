namespace DataMkt.Application.Producto.Dto;

public class ActualizarStockSucursalDto
{
    public int ProductoId { get; set; }
    public int SucursalId { get; set; }
    public int Cantidad { get; set; } // puede ser positiva o negativa
}