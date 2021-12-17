using Microsoft.EntityFrameworkCore.Migrations;

namespace ArysolDeportes_SantiagoFritz.Migrations
{
    public partial class OrdenarNombres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contenido",
                table: "Rutas",
                newName: "Contenido");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rutas",
                newName: "IdRuta");

            migrationBuilder.RenameColumn(
                name: "precio",
                table: "Productos",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Productos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Productos",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Productos",
                newName: "IdProducto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Imagenes",
                newName: "IdImagen");

            migrationBuilder.RenameColumn(
                name: "nombreCategoria",
                table: "Categorias",
                newName: "NombreCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contenido",
                table: "Rutas",
                newName: "contenido");

            migrationBuilder.RenameColumn(
                name: "IdRuta",
                table: "Rutas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Productos",
                newName: "precio");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Productos",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Productos",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "Productos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdImagen",
                table: "Imagenes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NombreCategoria",
                table: "Categorias",
                newName: "nombreCategoria");
        }
    }
}
