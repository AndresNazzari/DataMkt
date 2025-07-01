using Microsoft.AspNetCore.Http;

namespace DataMkt.Application.Ventas.Dto;

public class ImportarVentasRequest
{
    public IFormFile? File { get; set; }
}