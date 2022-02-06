using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPIServer.Migrations
{
    public partial class NomeCapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Filmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Capa",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Filmes");

            migrationBuilder.DropColumn(
                name: "Capa",
                table: "Filmes");
        }
    }
}
