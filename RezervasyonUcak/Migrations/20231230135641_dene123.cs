using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezervasyonUcak.Migrations
{
    /// <inheritdoc />
    public partial class dene123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koltuk_Ucak_UcakId",
                table: "Koltuk");

            migrationBuilder.AlterColumn<int>(
                name: "UcakId",
                table: "Koltuk",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Koltuk_Ucak_UcakId",
                table: "Koltuk",
                column: "UcakId",
                principalTable: "Ucak",
                principalColumn: "UcakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koltuk_Ucak_UcakId",
                table: "Koltuk");

            migrationBuilder.AlterColumn<int>(
                name: "UcakId",
                table: "Koltuk",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Koltuk_Ucak_UcakId",
                table: "Koltuk",
                column: "UcakId",
                principalTable: "Ucak",
                principalColumn: "UcakId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
