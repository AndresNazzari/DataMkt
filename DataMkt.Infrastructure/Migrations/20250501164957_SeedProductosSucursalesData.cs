using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMkt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductosSucursalesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sucursal Centro" },
                    { 2, "Sucursal Norte" }
                });

            // Insertar Productos (por si no estaban en una migración anterior)
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "Stock", "Precio" },
                values: new object[,]
                {
                    { 1, "Teclado", 0, 2500.00m },
                    { 2, "Mouse", 0, 1500.00m },
                    { 3, "Monitor", 0, 35000.00m },
                    { 4, "Notebook", 0, 150000.00m }
                });

            // Insertar Stock por Sucursal
            migrationBuilder.InsertData(
                table: "StocksPorSucursal",
                columns: new[] { "Id", "ProductoId", "SucursalId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 1, 20 },
                    { 2, 2, 1, 35 },
                    { 3, 3, 2, 10 },
                    { 4, 4, 2, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Borrar Stock por Sucursal (respetar orden por FK)
            migrationBuilder.DeleteData(
                table: "StocksPorSucursal",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            // Borrar Productos
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            // Borrar Sucursales
            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}
