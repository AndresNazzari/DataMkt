using DataMkt.Application.Common.Events;
using DataMkt.Application.Producto.Services;
using DataMkt.Application.Sucursal.Services;
using DataMkt.Application.Ventas.EventHandlers;
using DataMkt.Application.Ventas.Events;
using DataMkt.Application.Ventas.Importers;
using DataMkt.Application.Ventas.Repositories;
using DataMkt.Application.Ventas.Services;
using DataMkt.Domain.Repositories;
using DataMkt.Infrastructure.Persistence;
using DataMkt.Infrastructure.Productos.Repositories;
using DataMkt.Infrastructure.Stocks.Repositories;
using DataMkt.Infrastructure.Sucursales.Repositories;
using DataMkt.Infrastructure.Ventas.Importers;
using DataMkt.Infrastructure.Ventas.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataMkt.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Strategy por defecto (CSV)
        services.AddScoped<IVentasImporterStrategy, CsvVentasImporter>();
        services.AddScoped<IVentasImporterStrategy, JsonVentasImporter>();

        // Event handlers
        services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        services.AddScoped<IEventHandler<VentasImportadasEvent>, VentasImportadasEventHandler>();

        // Servicios de aplicación
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<ISucursalService, SucursalService>();
        services.AddScoped<ImportarVentasService>();

        // Repositorios
        services.AddScoped<IVentaRepository, VentaRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IStockPorSucursalRepository, StockPorSucursalRepository>();
        services.AddScoped<ISucursalRepository, SucursalRepository>();
        
        return services;
    }
}