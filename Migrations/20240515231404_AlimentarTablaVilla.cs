using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa", new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5767), new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5752), "", 50, "Villa real", 5, 200.0 },
                    { 2, "", "Detalle de la villa maria", new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5770), new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5770), "", 120, "Villa Maria", 50, 500.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
