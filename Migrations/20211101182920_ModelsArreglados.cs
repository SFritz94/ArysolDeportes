using Microsoft.EntityFrameworkCore.Migrations;

namespace ArysolDeportes_SantiagoFritz.Migrations
{
    public partial class ModelsArreglados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Imagenes_imagenesidImagen",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Rutas");

            migrationBuilder.DropIndex(
                name: "IX_Productos_imagenesidImagen",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "imagenesidImagen",
                table: "Productos");

            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Imagenes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Productoid",
                table: "Imagenes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imagenes_Productoid",
                table: "Imagenes",
                column: "Productoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagenes_Productos_Productoid",
                table: "Imagenes",
                column: "Productoid",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagenes_Productos_Productoid",
                table: "Imagenes");

            migrationBuilder.DropIndex(
                name: "IX_Imagenes_Productoid",
                table: "Imagenes");

            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Imagenes");

            migrationBuilder.DropColumn(
                name: "Productoid",
                table: "Imagenes");

            migrationBuilder.AddColumn<int>(
                name: "imagenesidImagen",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rutas",
                columns: table => new
                {
                    IdRuta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagenidImagen = table.Column<int>(type: "int", nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutas", x => x.IdRuta);
                    table.ForeignKey(
                        name: "FK_Rutas_Imagenes_ImagenidImagen",
                        column: x => x.ImagenidImagen,
                        principalTable: "Imagenes",
                        principalColumn: "IdImagen",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_imagenesidImagen",
                table: "Productos",
                column: "imagenesidImagen");

            migrationBuilder.CreateIndex(
                name: "IX_Rutas_ImagenidImagen",
                table: "Rutas",
                column: "ImagenidImagen");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Imagenes_imagenesidImagen",
                table: "Productos",
                column: "imagenesidImagen",
                principalTable: "Imagenes",
                principalColumn: "IdImagen",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
