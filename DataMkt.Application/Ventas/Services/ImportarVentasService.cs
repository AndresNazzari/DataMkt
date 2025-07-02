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
    private readonly IEnumerable<IVentasImporterStrategy> _importers;
    private readonly ILogger<ImportarVentasService> _logger;
    private readonly IEventPublisher _eventPublisher;

    public ImportarVentasService(
        IVentaRepository repository,
        IEnumerable<IVentasImporterStrategy> importers,
        ILogger<ImportarVentasService> logger,
        IEventPublisher eventPublisher)
    {
        _repository= repository;
        _importers= importers;
        _logger= logger;
        _eventPublisher = eventPublisher;
    }

    /// <param name="stream"></param>
    /// <param name="formato">
    /// Identificador de formato (“csv”, “json”, …) recibido en la petición.
    /// </param>
    public async Task ImportarAsync(Stream stream, string formato)
    {
        // 1) Seleccionar la estrategia que admite el formato solicitado
        var importer = _importers.FirstOrDefault(i =>
            string.Equals(i.Formato, formato, StringComparison.OrdinalIgnoreCase));

        if (importer is null)
        {
            throw new NotSupportedException($"Formato '{formato}' no soportado.");
        }

        var ventasImportadas = await importer.ImportarVentasAsync(stream);

        var ventas = ventasImportadas
            .Where(d => d.Cantidad > 0 && d.PrecioUnitario > 0)
            .Select(d => new Venta
            {
                Fecha = d.Fecha,
                ProductoId = d.ProductoId,
                SucursalId = d.SucursalId,
                Cantidad = d.Cantidad,
                PrecioUnitario= d.PrecioUnitario
            })
            .ToList();

        await _repository.AddRangeAsync(ventas);
        await _repository.SaveChangesAsync();

        await _eventPublisher.PublishAsync(new VentasImportadasEvent());
        _logger.LogInformation("📣 Evento publicado: VentasImportadasEvent");
    }

    public Task<List<VentaDto>> ObtenerVentasAsync() => _repository.GetVentasAsync();
}