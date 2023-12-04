using System;
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
                name: "Musteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.Id);
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
                    UcakFK = table.Column<int>(type: "integer", nullable: false),
                    ModelNumara = table.Column<string>(type: "text", nullable: false),
                    KoltukSayisi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UcakModel", x => x.UcakModelId);
                    table.ForeignKey(
                        name: "FK_UcakModel_Ucak_UcakFK",
                        column: x => x.UcakFK,
                        principalTable: "Ucak",
                        principalColumn: "UcakId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UcusSefers",
                columns: table => new
                {
                    UcusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaslangicSaat = table.Column<string>(type: "text", nullable: false),
                    VarisSaati = table.Column<string>(type: "text", nullable: false),
                    UcakId = table.Column<int>(type: "integer", nullable: false),
                    BaslangicKonum = table.Column<string>(type: "text", nullable: false),
                    VarisKonum = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UcusSefers", x => x.UcusId);
                    table.ForeignKey(
                        name: "FK_UcusSefers_Ucak_UcakId",
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

            migrationBuilder.CreateTable(
                name: "Bilets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UcusSeferId = table.Column<int>(type: "integer", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    KesimTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BiletFiyat = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bilets_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bilets_UcusSefers_UcusSeferId",
                        column: x => x.UcusSeferId,
                        principalTable: "UcusSefers",
                        principalColumn: "UcusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_MusteriId",
                table: "Bilets",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilets_UcusSeferId",
                table: "Bilets",
                column: "UcusSeferId");

            migrationBuilder.CreateIndex(
                name: "IX_Koltuk_UcakModelId",
                table: "Koltuk",
                column: "UcakModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucak_FirmaId",
                table: "Ucak",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_UcakModel_UcakFK",
                table: "UcakModel",
                column: "UcakFK");

            migrationBuilder.CreateIndex(
                name: "IX_UcusSefers_UcakId",
                table: "UcusSefers",
                column: "UcakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilets");

            migrationBuilder.DropTable(
                name: "Koltuk");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "UcusSefers");

            migrationBuilder.DropTable(
                name: "UcakModel");

            migrationBuilder.DropTable(
                name: "Ucak");

            migrationBuilder.DropTable(
                name: "Firma");
        }
    }
}
