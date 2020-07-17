using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VintageStuff.Data.Migrations
{
    public partial class Darwin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Foto = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RopaFotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RopaFotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    BodegueroId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodegas_Personas_BodegueroId",
                        column: x => x.BodegueroId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zapatos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Foto = table.Column<byte[]>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zapatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zapatos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boxeres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FotoId = table.Column<int>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    BodegaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxeres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxeres_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boxeres_RopaFotos_FotoId",
                        column: x => x.FotoId,
                        principalTable: "RopaFotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Camisetas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Foto = table.Column<byte[]>(nullable: true),
                    BodegaId = table.Column<int>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camisetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camisetas_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Camisetas_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pantalones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Talla = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Foto = table.Column<byte[]>(nullable: true),
                    BodegaId = table.Column<int>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantalones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pantalones_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pantalones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Talla = table.Column<string>(nullable: true),
                    BodegaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polines_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Topes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FotoId = table.Column<int>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    BodegaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topes_Bodegas_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodegas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Topes_RopaFotos_FotoId",
                        column: x => x.FotoId,
                        principalTable: "RopaFotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_BodegueroId",
                table: "Bodegas",
                column: "BodegueroId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxeres_BodegaId",
                table: "Boxeres",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxeres_FotoId",
                table: "Boxeres",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Camisetas_BodegaId",
                table: "Camisetas",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Camisetas_PersonaId",
                table: "Camisetas",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pantalones_BodegaId",
                table: "Pantalones",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pantalones_PersonaId",
                table: "Pantalones",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Polines_BodegaId",
                table: "Polines",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Topes_BodegaId",
                table: "Topes",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Topes_FotoId",
                table: "Topes",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Zapatos_PersonaId",
                table: "Zapatos",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxeres");

            migrationBuilder.DropTable(
                name: "Camisetas");

            migrationBuilder.DropTable(
                name: "Pantalones");

            migrationBuilder.DropTable(
                name: "Polines");

            migrationBuilder.DropTable(
                name: "Topes");

            migrationBuilder.DropTable(
                name: "Zapatos");

            migrationBuilder.DropTable(
                name: "Bodegas");

            migrationBuilder.DropTable(
                name: "RopaFotos");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
