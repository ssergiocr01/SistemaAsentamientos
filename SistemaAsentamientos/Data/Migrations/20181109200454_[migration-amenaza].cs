using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaAsentamientos.Data.Migrations
{
    public partial class migrationamenaza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmenazaNatural",
                columns: table => new
                {
                    AmenazaNaturalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenazaNatural", x => x.AmenazaNaturalID);
                });

            migrationBuilder.CreateTable(
                name: "CatalogoAmenaza",
                columns: table => new
                {
                    CatalogoAmenazaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmenazaNaturalID = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoAmenaza", x => x.CatalogoAmenazaID);
                    table.ForeignKey(
                        name: "FK_CatalogoAmenaza_AmenazaNatural_AmenazaNaturalID",
                        column: x => x.AmenazaNaturalID,
                        principalTable: "AmenazaNatural",
                        principalColumn: "AmenazaNaturalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoAmenaza_AmenazaNaturalID",
                table: "CatalogoAmenaza",
                column: "AmenazaNaturalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogoAmenaza");

            migrationBuilder.DropTable(
                name: "AmenazaNatural");
        }
    }
}
