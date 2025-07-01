using DataMkt.Application.Common.Events;
using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Events;
using DataMkt.Application.Ventas.Importers;
using DataMkt.Application.Ventas.Repositories;
using DataMkt.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DataMkt.Application.Ventas.Services;

public class ImportarVentasService
{
    private readonly IVentaRepository _repository;
    private readonly IVentasImporterStrategy _importer;
    private readonly ILogger<ImportarVentasService> _logger;
    private readonly IEventPublisher _eventPublisher;

    public ImportarVentasService(
        IVentaRepository repository,
        IVentasImporterStrategy importer,
        ILogger<ImportarVentasService> logger,
        IEventPublisher eventPublisher)
    {
        _repository = repository;
        _importer = importer;
        _logger = logger;
        _eventPublisher = eventPublisher;
    }

    public async Task ImportarAsync(Stream stream)
    {
        var ventasImportadas = await _importer.ImportarVentasAsync(stream);

        var ventas = ventasImportadas
            .Where(dto => dto.Cantidad > 0 && dto.PrecioUnitario > 0)
            .Select(dto => new Venta
            {
                Fecha = dto.Fecha,
                ProductoId = dto.ProductoId,
                SucursalId = dto.SucursalId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            })
            .ToList();

        await _repository.AddRangeAsync(ventas);
        await _repository.SaveChangesAsync();

        await _eventPublisher.PublishAsync(new VentasImportadasEvent());
        _logger.LogInformation("📣 Evento publicado: VentasImportadasEvent");
    }
    
    public async Task<List<VentaDto>> ObtenerVentasAsync()
    {
        return await _repository.GetVentasAsync();
    }
}