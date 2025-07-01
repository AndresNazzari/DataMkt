using DataMkt.Application.Ventas.Dto;

namespace DataMkt.Application.Ventas.Importers;

public interface IVentasImporterStrategy
{
    Task<List<VentaImportadaDto>> ImportarVentasAsync(Stream sourceStream);
}