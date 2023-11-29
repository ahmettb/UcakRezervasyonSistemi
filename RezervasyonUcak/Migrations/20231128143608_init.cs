using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RezervasyonUcak.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaAdi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.FirmaId);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucak",
                columns: table => new
                {
                    UcakId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucak", x => x.UcakId);
                    table.ForeignKey(
                        name: "FK_Ucak_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "FirmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UcakModel",
                columns: table => new
                {
                    UcakModelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelNumara = table.Column<string>(type: "text", nullable: false),
                    KoltukSayisi = table.Column<int>(type: "integer", nullable: false),
                    UcakId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UcakModel", x => x.UcakModelId);
                    table.ForeignKey(
                        name: "FK_UcakModel_Ucak_UcakId",
                        column: x => x.UcakId,
                        principalTable: "Ucak",
                        principalColumn: "UcakId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Koltuk",
                columns: table => new
                {
                    KoltukId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UcakModelId = table.Column<int>(type: "integer", nullable: false),
                    DoluMu = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koltuk", x => x.KoltukId);
                    table.ForeignKey(
                        name: "FK_Koltuk_UcakModel_UcakModelId",
                        column: x => x.UcakModelId,
                        principalTable: "UcakModel",
                        principalColumn: "UcakModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Koltuk_UcakModelId",
                table: "Koltuk",
                column: "UcakModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucak_FirmaId",
                table: "Ucak",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_UcakModel_UcakId",
                table: "UcakModel",
                column: "UcakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Koltuk");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "UcakModel");

            migrationBuilder.DropTable(
                name: "Ucak");

            migrationBuilder.DropTable(
                name: "Firma");
        }
    }
}
