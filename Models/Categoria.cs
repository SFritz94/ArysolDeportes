using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArysolDeportes_SantiagoFritz.Models
{
    public class Categoria
    {
        [Key]
        [Column("IdCategoria")]
        public int idCategoria {get;set;}
        [Column("NombreCategoria")]
        public string nombreCategoria {get;set;}
    }
}