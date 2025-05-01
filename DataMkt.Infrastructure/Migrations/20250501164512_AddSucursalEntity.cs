using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMkt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSucursalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StocksPorSucursal_SucursalId",
                table: "StocksPorSucursal",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_StocksPorSucursal_Sucursales_SucursalId",
                table: "StocksPorSucursal",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StocksPorSucursal_Sucursales_SucursalId",
                table: "StocksPorSucursal");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_StocksPorSucursal_SucursalId",
                table: "StocksPorSucursal");
        }
    }
}
