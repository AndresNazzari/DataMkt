using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataMkt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel20250702 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
