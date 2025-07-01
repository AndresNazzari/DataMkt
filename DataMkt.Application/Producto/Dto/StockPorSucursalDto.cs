namespace DataMkt.Application.Producto.Dto;

public record StockPorSucursalDto
{
    public string Producto { get; init; } = string.Empty;
    public string Sucursal { get; init; } = string.Empty;
    public int Stock { get; init; }
};