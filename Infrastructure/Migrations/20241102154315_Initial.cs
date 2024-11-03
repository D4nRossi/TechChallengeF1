using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONTATO_CTT",
                columns: table => new
                {
                    CTT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTT_DTCRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CTT_NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTT_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTT_DDD = table.Column<int>(type: "int", nullable: false),
                    CTT_NUMERO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTATO_CTT", x => x.CTT_ID);
                });

            migrationBuilder.CreateTable(
                name: "MUNICIPIO_MNC",
                columns: table => new
                {
                    MNC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MNC_NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MNC_LAT = table.Column<float>(type: "real", nullable: false),
                    MNC_LONG = table.Column<float>(type: "real", nullable: false),
                    MNC_CAPITAL = table.Column<bool>(type: "bit", nullable: false),
                    MNC_UF = table.Column<int>(type: "int", nullable: false),
                    MNC_SIAFI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MNC_DDD = table.Column<int>(type: "int", nullable: false),
                    MNC_IBGE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MUNICIPIO_MNC", x => x.MNC_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTATO_CTT");

            migrationBuilder.DropTable(
                name: "MUNICIPIO_MNC");
        }
    }
}
