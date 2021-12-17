using Microsoft.EntityFrameworkCore.Migrations;

namespace ArysolDeportes_SantiagoFritz.Migrations
{
    public partial class ProductoEliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "eliminado",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eliminado",
                table: "Productos");
        }
    }
}
