namespace DataMkt.Application.Ventas.Dto;

public class VentaImportadaDto
{
    public DateTime Fecha { get; set; }
    public int ProductoId { get; set; }
    public int SucursalId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}