using System.ComponentModel.DataAnnotations;

namespace DataMkt.Application.Producto.Dto;

/// <summary>
/// DTO utilizado para actualizar el stock de un producto en una sucursal.
/// </summary>
public class ActualizarStockSucursalDto
{
    /// <summary>
    /// ID del producto a actualizar.
    /// </summary>
    /// <example>1</example>
    public int ProductoId { get; set; }

    /// <summary>
    /// ID de la sucursal donde se actualizará el stock.
    /// </summary>
    /// <example>2</example>
    public int SucursalId { get; set; }

    /// <summary>
    /// Cantidad a modificar (positiva para sumar stock, negativa para restar).
    /// </summary>
    /// <example>+10</example>
    public int Cantidad { get; set; }
}