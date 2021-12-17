using Microsoft.EntityFrameworkCore;

namespace ArysolDeportes_SantiagoFritz.Models
{
    public class ArysolDeportesDbContext : DbContext
    {
        public virtual DbSet<Categoria> Categorias{get;set;}
        public virtual DbSet<Imagen> Imagenes{get;set;}
        public virtual DbSet<Producto> Productos{get;set;}
        public ArysolDeportesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ArysolDeportesDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

        }

        /*partial void OnModelCreatingPartial(ModelBuilder modelBuilder){

        }*/
    }
}