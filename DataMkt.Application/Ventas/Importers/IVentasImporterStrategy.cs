using DataMkt.Application.Ventas.Dto;

namespace DataMkt.Application.Ventas.Importers;

public interface IVentasImporterStrategy
{
    string? Formato { get; }
    Task<List<VentaImportadaDto>> ImportarVentasAsync(Stream sourceStream);
}