using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    villaNo = table.Column<int>(type: "int", nullable: false),
                    villaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.villaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_villaId",
                        column: x => x.villaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4908), new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4895) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4912), new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4912) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_villaId",
                table: "NumeroVillas",
                column: "villaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5767), new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5752) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5770), new DateTime(2024, 5, 15, 18, 14, 4, 651, DateTimeKind.Local).AddTicks(5770) });
        }
    }
}
