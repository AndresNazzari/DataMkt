using DataMkt.Application.Common.Events;
using DataMkt.Application.Ventas.Events;
using DataMkt.Application.Ventas.Repositories;
using OfficeOpenXml;

namespace DataMkt.Application.Ventas.EventHandlers;

public class VentasImportadasEventHandler : IEventHandler<VentasImportadasEvent>
{
    private readonly IVentaRepository _repository;

    public VentasImportadasEventHandler(IVentaRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(VentasImportadasEvent @event)
    {
        var ventas = await _repository.GetVentasAsync();

        var filePath = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
        using var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Ventas");

        sheet.Cells[1, 1].Value = "Fecha";
        sheet.Cells[1, 2].Value = "Producto";
        sheet.Cells[1, 3].Value = "Sucursal";
        sheet.Cells[1, 4].Value = "Cantidad";
        sheet.Cells[1, 5].Value = "Precio";
        sheet.Cells[1, 6].Value = "Total";

        for (int i = 0; i < ventas.Count; i++)
        {
            var v = ventas[i];
            sheet.Cells[i + 2, 1].Value = v.Fecha.ToShortDateString();
            sheet.Cells[i + 2, 2].Value = v.Producto;
            sheet.Cells[i + 2, 3].Value = v.Sucursal;
            sheet.Cells[i + 2, 4].Value = v.Cantidad;
            sheet.Cells[i + 2, 5].Value = v.PrecioUnitario;
            sheet.Cells[i + 2, 6].Value = v.Total;
        }

        Directory.CreateDirectory("Reportes");
        await package.SaveAsAsync(new FileInfo(Path.Combine("Reportes", filePath)));
    }
}