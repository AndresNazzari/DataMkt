using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DataMkt.Application.Ventas.Dto;
using DataMkt.Application.Ventas.Importers;

namespace DataMkt.Infrastructure.Ventas.Importers;

public class CsvVentasImporter  : IVentasImporterStrategy
{
    public string Formato => "csv";
    
    public async Task<List<VentaImportadaDto>> ImportarVentasAsync(Stream sourceStream)
    {
        using var reader = new StreamReader(sourceStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        var records = csv.GetRecords<VentaImportadaDto>().ToList();
        return await Task.FromResult(records);
    }
}