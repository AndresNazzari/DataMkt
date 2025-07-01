using DataMkt.Application.Producto.Dto;
using DataMkt.Application.Producto.Mapping;
using DataMkt.Domain.Entities;
using DataMkt.Domain.Repositories;

namespace DataMkt.Application.Producto.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepo;
    private readonly IStockPorSucursalRepository _stockRepo;

    public ProductoService(IProductoRepository productoRepo, IStockPorSucursalRepository stockRepo)
    {
        _productoRepo = productoRepo;
        _stockRepo = stockRepo;
    }

    public async Task<IEnumerable<ProductoDto>> GetProductosAsync(CancellationToken ct = default)
        => (await _productoRepo.GetAllAsync(ct)).Select(p => p.ToDto());

    public async Task<IEnumerable<StockPorSucursalDto>> GetStockPorSucursalAsync(CancellationToken ct = default)
    {
        var list = await _stockRepo.GetAllWithIncludesAsync(ct);
        return list.Select(s => new StockPorSucursalDto
        {
            Producto = s.Producto!.Nombre,
            Sucursal = s.Sucursal!.Nombre,
            Stock = s.Stock
        });
    }

    public async Task<ProductoDto> CreateProductoAsync(ProductoDto dto, CancellationToken ct = default)
    {
        var entity = dto.ToEntity();
        var added = await _productoRepo.AddAsync(entity, ct);
        return added.ToDto();
    }

    public async Task ActualizarStockAsync(ActualizarStockSucursalDto dto, CancellationToken ct = default)
    {
        var stock = await _stockRepo.FindAsync(dto.ProductoId, dto.SucursalId, ct);
        if (stock is null)
        {
            stock = new StockPorSucursal
            {
                ProductoId = dto.ProductoId,
                SucursalId = dto.SucursalId,
                Stock = dto.Cantidad
            };
            await _stockRepo.AddAsync(stock, ct);
        }
        else
        {
            stock.Stock += dto.Cantidad;
        }

        await _stockRepo.SaveChangesAsync(ct);
    }
}