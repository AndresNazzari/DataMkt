namespace DataMkt.Application.Producto.Dto;

/// <summary>
/// DTO expuesto por la capa de Aplicación para representar un producto sin filtrar ni exponer la entidad de dominio.
/// </summary>
public record ProductoDto
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public decimal Precio { get; init; }
};