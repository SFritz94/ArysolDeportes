using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArysolDeportes_SantiagoFritz.Models
{
    public class Paginador
    {
        public int PaginaActual {get;set;}
        public int TotalDeProductos {get;set;}
        public int ProductosPorPagina {get;set;}
    }
}