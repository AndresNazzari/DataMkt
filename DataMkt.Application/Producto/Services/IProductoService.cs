using DataMkt.Application.Producto.Dto;

namespace DataMkt.Application.Producto.Services;

public interface IProductoService
{
    Task<IEnumerable<ProductoDto>> GetProductosAsync(CancellationToken ct = default);
    Task<IEnumerable<StockPorSucursalDto>> GetStockPorSucursalAsync(CancellationToken ct = default);
    Task<ProductoDto> CreateProductoAsync(ProductoDto dto, CancellationToken ct = default);
    Task ActualizarStockAsync(ActualizarStockSucursalDto dto, CancellationToken ct = default);
}
