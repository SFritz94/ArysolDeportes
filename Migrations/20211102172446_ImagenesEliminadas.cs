using Microsoft.EntityFrameworkCore.Migrations;

namespace ArysolDeportes_SantiagoFritz.Migrations
{
    public partial class ImagenesEliminadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminada",
                table: "Imagenes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminada",
                table: "Imagenes");
        }
    }
}
