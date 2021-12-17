using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArysolDeportes_SantiagoFritz.Models
{
    public class Imagen
    {
        [Key]
        [Column("IdImagen")]
        public int idImagen {get;set;}
        
        [Column("Contenido")]
        public string contenido {get;set;}

        [Column("Eliminada")]
        public bool eliminada {get;set;}

        [Column("Productoid")]
        public int productoId {get;set;}

        public Imagen(){
        }
        public Imagen(string contenido){
            this.contenido=contenido;
        }

    }
}