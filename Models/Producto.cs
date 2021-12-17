using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ArysolDeportes_SantiagoFritz.Models
{
    public class Producto
    {
        [Key]
        [Column("IdProducto")]
        public int id {get;set;}

        [Display(Name = "Imagenes")]
        [Required(ErrorMessage ="Por favor ingrese al menos una imagen.")]
        [Column("Imagenes_IdImagenes")]
        public List<Imagen> imagenes {get;set;}

        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="Por favor ingrese un nombre.")]
        [RegularExpression("([a-zA-Z0-9_ .&'-]+)", ErrorMessage = "Por favor solo ingresar caracteres alfanumericos.")]
        [Column("Nombre")]
        public string nombre {get;set;}

        [Display(Name = "Categoria")]
        [Required]
        [Column("Categoria_IdCategoria")]
        public Categoria categoria{get;set;}

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage ="Por favor ingrese una descripcion.")]
        //[RegularExpression("([a-zA-Z0-9_ .&'-]+)", ErrorMessage = "Por favor solo ingresar caracteres alfanumericos.")]
        [MaxLength(100, ErrorMessage ="La descripcion solo puede contar con un maximo de 100 caracteres.")]
        [MinLength(10, ErrorMessage ="La descripcion debe tener como minimo 10 caracteres.")]
        [Column("Descripcion")]
        public string descripcion {get;set;}

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Por favor ingrese un precio.")]
        [Range(1,100000, ErrorMessage ="El precio debe estar en el rango de 1 y 100000.")]
        [Column("Precio")]
        public double? precio {get;set;}

        [Display(Name = "Cantidad disponible")]
        [Column("Stock")]
        [Required(ErrorMessage ="Por favor ingrese una cantidad disponible.")]
        [Range(1,100000, ErrorMessage ="La cantidad disponible debe estar en el rango de 1 y 100000.")]
        public int? stock {get;set;}

        [Column("Eliminado")]
        public bool eliminado {get;set;}

        [NotMapped()]
        public List<IFormFile> listaImagenes {get;set;}

        public void agregarImagen(Imagen imagen){
            if(this.imagenes==null){
                this.imagenes = new List<Imagen>();
                this.imagenes.Add(imagen);
            }else{
                this.imagenes.Add(imagen);
            }
        }

        public void agregarListaImagen(List<Imagen> imagenes){
            if(this.imagenes==null){
                this.imagenes = new List<Imagen>();
                this.imagenes.AddRange(imagenes);
            }else{
                this.imagenes.AddRange(imagenes);
            }
        }
    }
}