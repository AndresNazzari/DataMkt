using System.Text.Json;
using System.Text.Json.Serialization;
using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Importers;

namespace DataMkt.Infrastructure.Ventas.Importers;

public class JsonVentasImporter : IVentasImporterStrategy
{
    public string Formato => "json";
    
    public async Task<List<VentaImportadaDto>> ImportarVentasAsync(Stream sourceStream)
    {
        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var ventas = await JsonSerializer.DeserializeAsync<List<VentaImportadaDto>>(
                         sourceStream, opciones) 
                     ?? new List<VentaImportadaDto>();

        return ventas;
    }
}