using DataMkt.Application.Common.Events;
using DataMkt.Application.Ventas.EventHandlers;
using DataMkt.Application.Ventas.Events;
using DataMkt.Application.Ventas.Importers;
using DataMkt.Application.Ventas.Repositories;
using DataMkt.Application.Ventas.Services;
using DataMkt.Infrastructure.Persistence;
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

        // Aquí se pueden registrar repositorios u otros servicios
        // Strategy por defecto (CSV)
        services.AddScoped<IVentasImporterStrategy, CsvVentasImporter>();
        services.AddScoped<IVentaRepository, VentaRepository>();
        services.AddScoped<ImportarVentasService>();

        services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        services.AddScoped<IEventHandler<VentasImportadasEvent>, VentasImportadasEventHandler>();

        return services;
    }
}