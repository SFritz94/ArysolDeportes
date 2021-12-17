using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using ArysolDeportes_SantiagoFritz.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ArysolDeportes_SantiagoFritz.Controllers
{
    public class ProductosController : Controller
    {
        const int CANTIDAD_PRODUCTOS_POR_PAGINA = 3;
        private readonly IWebHostEnvironment Hosting;
        private readonly ArysolDeportesDbContext Db;
        public ProductosController(IWebHostEnvironment hosting, ArysolDeportesDbContext ctx)
        {
            Db = ctx;
            Hosting = hosting;
        }
        public IActionResult Index(string campoOrden, string campoBusqueda, int pagina)
        {
            //Con Include() agregamos las tablas relacionadas a la tabla que estamos buscando.
            //En este caso Productos.
            if(pagina != 0){//Indica en que pagina uno se encuentra, sino vamos a la pagina 1 por defecto.
                ViewBag.pagina = pagina;
            }else{
                pagina = 1;
            }

            if(!String.IsNullOrEmpty(campoOrden)){
                ViewBag.orden = campoOrden;
            }

            var cantidadProductosPorPagina = CANTIDAD_PRODUCTOS_POR_PAGINA;

            var listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                .OrderBy(prod => prod.id)
                                .Skip((pagina - 1) * cantidadProductosPorPagina)//Salta tantos productos como se indique en el parametro. En caso de estar en la pagina 2 saltara los 3 primeros productos. 
                                .Take(cantidadProductosPorPagina)//Funcion que toma los productos indicados. En este caso 3 productos.
                                .ToList();
            
            var totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                   .Include(prod => prod.categoria).Where(prod => prod.eliminado==false).Count();

            if(!(String.IsNullOrEmpty(campoBusqueda))){
                ViewBag.busqueda = campoBusqueda;
                listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                .Where(prod => prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                || prod.precio.ToString().Contains(campoBusqueda)
                                || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())
                                ).OrderBy(prod => prod.id)
                                .Skip((pagina - 1) * cantidadProductosPorPagina)
                                .Take(cantidadProductosPorPagina)
                                .ToList();

                totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                   .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                   .Where(prod => prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                   || prod.precio.ToString().Contains(campoBusqueda)
                                   || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())
                                   ).OrderBy(prod => prod.id)
                                   .Count();
            }

            switch(campoOrden){
                case "nombre":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                        .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                        && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                        || prod.precio.ToString().Contains(campoBusqueda)
                                        || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                        .OrderBy(prod=>prod.nombre)
                                        .Skip((pagina - 1) * cantidadProductosPorPagina)
                                        .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                           .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                           && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                           || prod.precio.ToString().Contains(campoBusqueda)
                                           || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())).OrderBy(prod => prod.id)
                                           .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                        .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                        .OrderBy(prod=>prod.nombre)
                                        .Skip((pagina - 1) * cantidadProductosPorPagina)
                                        .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                           .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                           .OrderBy(prod => prod.id)
                                           .Count();
                    }
                    break;
                case "menorPrecio":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                    && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                    || prod.precio.ToString().Contains(campoBusqueda)
                                    || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                    .OrderBy(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                       && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                       || prod.precio.ToString().Contains(campoBusqueda)
                                       || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                    .OrderBy(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==false)
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }
                    break;
                case "mayorPrecio":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                    && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                    || prod.precio.ToString().Contains(campoBusqueda)
                                    || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                    .OrderByDescending(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==false
                                       && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                       || prod.precio.ToString().Contains(campoBusqueda)
                                       || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==false).OrderByDescending(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==false).OrderBy(prod => prod.precio)
                                       .Count();
                    }
                    break;
            }

            var productosPaginados = new IndexViewModel();//Instanciamos el modelo paginado.

            productosPaginados.Productos = listaOrdenada;//Cargamos la lista de productos.
            productosPaginados.PaginaActual = pagina;//Cargamos la pagina donde nos encontramos.
            productosPaginados.TotalDeProductos = totalDeProductos;//Cargamos el total de productos.
            productosPaginados.ProductosPorPagina = cantidadProductosPorPagina;//Cargamos la cantidad de productos por pagina.

            ModelState.Clear();
            return View(productosPaginados);
        }

        public IActionResult DetalleProducto(int id, int pagina, string campoOrden, string campoBusqueda){

            if(pagina != 0){
                ViewBag.pagina = pagina;
            }else{
                pagina = 1;
            }

            if(!String.IsNullOrEmpty(campoBusqueda)){
                ViewBag.busqueda = campoBusqueda;
            }

            if(!String.IsNullOrEmpty(campoOrden)){
                ViewBag.orden = campoOrden;
            }

            var productoBuscado = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false)).Include(prod => prod.categoria).Where(prod => prod.id == id).FirstOrDefault();
            ViewBag.carouselDetalle=1;
            return View(productoBuscado);
        }

        public IActionResult Editar(int id, string prodElim, int pagina, string campoOrden, string campoBusqueda){

            if(pagina != 0){
                ViewBag.pagina = pagina;
            }else{
                pagina = 1;
            }

            if(!String.IsNullOrEmpty(campoBusqueda)){
                ViewBag.busqueda = campoBusqueda;
            }

            if(!String.IsNullOrEmpty(campoOrden)){
                ViewBag.orden = campoOrden;
            }

            var categoriasDb = Db.Categorias.ToList();
            var productoBuscado = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false)).Include(prod => prod.categoria).Where(prod => prod.id == id).FirstOrDefault();
            ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
            ViewBag.altaProdElim = prodElim;
            ViewBag.carouselDetalle=1;
            return View(productoBuscado);
        }

        [HttpPost]
        public IActionResult Editar(Producto productoForm, string prodElim){

            var categoriasDb = Db.Categorias.ToList();
            ViewBag.altaProdElim = prodElim;
            ViewBag.carouselDetalle=1;
            //Validamos si el model es correcto y si existe alguna imagen en el producto o si se subio algun archivo.
            if(ModelState.IsValid && (TieneAlgunIFormFile(productoForm) || NoTodosLosElementosSonNull(productoForm.imagenes))){
                //Validmaos si el producto contiene alguna imagen null.
                if(ImagenesContieneNull(productoForm.imagenes)){
                    //Validamos si se subio algun archivo y si los archivos subidos son todos del tipo imagen.
                    if(TieneAlgunIFormFile(productoForm) && LosArchivosSonSoloImagenes(productoForm)){
                        QuitarImagenes(productoForm);
                        AgregarImagenes(productoForm);
                        productoForm.categoria=AsignarCategoria(productoForm);
                        AltaProductoEliminadoEnDb(productoForm);
                        ActualizarProductoEnDb(productoForm);
                        if(!String.IsNullOrEmpty(prodElim)){
                            return RedirectToAction("AltaProductoEliminado");
                        }
                        return RedirectToAction("Index");
                    }
                    //Validamos si se subio algun archivo y si existe algun archivo que no sea del tipo imagen.
                    if(TieneAlgunIFormFile(productoForm) && !LosArchivosSonSoloImagenes(productoForm)){
                        ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
                        ReconstruirImagenes(productoForm);
                        return View(productoForm);
                    }
                    //Si contiene alguna imagen null pero no se subieron archivos realiza lo siguiente.
                    QuitarImagenes(productoForm);
                    productoForm.categoria=AsignarCategoria(productoForm);
                    AltaProductoEliminadoEnDb(productoForm);
                    ActualizarProductoEnDb(productoForm);
                    if(!String.IsNullOrEmpty(prodElim)){
                            return RedirectToAction("AltaProductoEliminado");
                    }
                    return RedirectToAction("Index");
                }else{//Si el producto no contiene alguna imagen null continua aqui.
                    //Validamos si se subio algun archivo y si los archivos subidos son solo imagenes.
                    if((TieneAlgunIFormFile(productoForm) && LosArchivosSonSoloImagenes(productoForm))){
                        AgregarImagenes(productoForm);
                        productoForm.categoria=AsignarCategoria(productoForm);
                        AltaProductoEliminadoEnDb(productoForm);
                        ActualizarProductoEnDb(productoForm);
                        if(!String.IsNullOrEmpty(prodElim)){
                            return RedirectToAction("AltaProductoEliminado");
                        }
                        return RedirectToAction("Index");
                    }
                    //Validamos si se subio algun archivo y si los archivos subidos no son solo imagenes.
                    if(TieneAlgunIFormFile(productoForm) && !LosArchivosSonSoloImagenes(productoForm)){
                        ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
                        return View(productoForm);
                    }
                    //Si no se entra en ninguna de las validaciones anteriores se continua por aqui.
                    productoForm.categoria=AsignarCategoria(productoForm);
                    //Funcion que habilita un producto si ya existe en la base de datos.
                    AltaProductoEliminadoEnDb(productoForm);
                    ActualizarProductoEnDb(productoForm);
                    if(!String.IsNullOrEmpty(prodElim)){
                            return RedirectToAction("AltaProductoEliminado");
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
            if(!NoTodosLosElementosSonNull(productoForm.imagenes)){
                ModelState.AddModelError("imagenes", "El producto debe tener al menos una imagen.");
            }
            if(ImagenesContieneNull(productoForm.imagenes)){
                ReconstruirImagenes(productoForm);
            }
            return View(productoForm);
        }

        public IActionResult Crear(int pagina, string campoOrden, string campoBusqueda){

            if(pagina != 0){
                ViewBag.pagina = pagina;
            }else{
                pagina = 1;
            }

            if(!String.IsNullOrEmpty(campoBusqueda)){
                ViewBag.busqueda = campoBusqueda;
            }

            if(!String.IsNullOrEmpty(campoOrden)){
                ViewBag.orden = campoOrden;
            }

            var categoriasDb = Db.Categorias.ToList();
            ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Producto productoForm){
            var categoriasDb = Db.Categorias.ToList();
            if(ModelState.IsValid || TieneAlgunIFormFile(productoForm)){
                if(LosArchivosSonSoloImagenes(productoForm)){
                    productoForm.categoria = AsignarCategoria(productoForm);
                    Db.Productos.Add(productoForm);
                    Db.SaveChanges();
                    AgregarImagenes(productoForm);
                    return RedirectToAction("Index");
                    }
                ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
                productoForm = null;
                return View(productoForm);
            }
            ViewBag.categorias = new SelectList(categoriasDb,"idCategoria","nombreCategoria");
            productoForm = null;
            return View(productoForm);
        }

        public IActionResult AltaProductoEliminado(string campoOrden, string campoBusqueda, int pagina){
            
            if(pagina != 0){
                ViewBag.pagina = pagina;
            }else{
                pagina = 1;
            }

            ViewBag.orden = campoOrden;

            var cantidadProductosPorPagina = CANTIDAD_PRODUCTOS_POR_PAGINA;

            var listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                .OrderBy(prod => prod.id)
                                .Skip((pagina - 1) * cantidadProductosPorPagina)
                                .Take(cantidadProductosPorPagina)
                                .ToList();
            
            var totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                   .Include(prod => prod.categoria).Where(prod => prod.eliminado==true).Count();

            if(!(String.IsNullOrEmpty(campoBusqueda))){
                ViewBag.busqueda = campoBusqueda;
                listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                .Where(prod => prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                || prod.precio.ToString().Contains(campoBusqueda)
                                || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())
                                ).OrderBy(prod => prod.id)
                                .Skip((pagina - 1) * cantidadProductosPorPagina)
                                .Take(cantidadProductosPorPagina)
                                .ToList();

                totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                   .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                   .Where(prod => prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                   || prod.precio.ToString().Contains(campoBusqueda)
                                   || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())
                                   ).OrderBy(prod => prod.id)
                                   .Count();
            }

            switch(campoOrden){
                case "nombre":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                        .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                        && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                        || prod.precio.ToString().Contains(campoBusqueda)
                                        || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                        .OrderBy(prod=>prod.nombre)
                                        .Skip((pagina - 1) * cantidadProductosPorPagina)
                                        .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                           .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                           && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                           || prod.precio.ToString().Contains(campoBusqueda)
                                           || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper())).OrderBy(prod => prod.id)
                                           .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                        .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                        .OrderBy(prod=>prod.nombre)
                                        .Skip((pagina - 1) * cantidadProductosPorPagina)
                                        .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                           .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                           .OrderBy(prod => prod.id)
                                           .Count();
                    }
                    break;
                case "menorPrecio":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                    && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                    || prod.precio.ToString().Contains(campoBusqueda)
                                    || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                    .OrderBy(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                       && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                       || prod.precio.ToString().Contains(campoBusqueda)
                                       || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                    .OrderBy(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();
                    
                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==true)
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }
                    break;
                case "mayorPrecio":
                    if(!String.IsNullOrEmpty(campoBusqueda)){
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                    && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                    || prod.precio.ToString().Contains(campoBusqueda)
                                    || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                    .OrderByDescending(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==true
                                       && prod.nombre.ToUpper().Contains(campoBusqueda.ToUpper())
                                       || prod.precio.ToString().Contains(campoBusqueda)
                                       || prod.categoria.nombreCategoria.ToUpper().Contains(campoBusqueda.ToUpper()))
                                       .OrderBy(prod => prod.precio)
                                       .Count();
                    }else{
                        listaOrdenada = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                    .Include(prod => prod.categoria).Where(prod => prod.eliminado==true).OrderByDescending(prod=>prod.precio)
                                    .Skip((pagina - 1) * cantidadProductosPorPagina)
                                    .Take(cantidadProductosPorPagina).ToList();

                        totalDeProductos = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false))
                                       .Include(prod => prod.categoria).Where(prod => prod.eliminado==true).OrderBy(prod => prod.precio)
                                       .Count();
                    }
                    break;
            }

            var productosPaginados = new IndexViewModel();

            productosPaginados.Productos = listaOrdenada;
            productosPaginados.PaginaActual = pagina;
            productosPaginados.TotalDeProductos = totalDeProductos;
            productosPaginados.ProductosPorPagina = cantidadProductosPorPagina;

            ModelState.Clear();
            ViewBag.altaProdElim = "prodElim";
            return View(productosPaginados);
        }

        public IActionResult Eliminar(int id){

            var productoBuscado = Db.Productos.Where(prod => prod.id == id).FirstOrDefault();
            productoBuscado.eliminado=true;
            Db.Productos.Update(productoBuscado);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void ActualizarProductoEnDb(Producto productoForm){
            
            var productoDB = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false)).Include(prod => prod.categoria).Where(prod => prod.id == productoForm.id).FirstOrDefault();
            productoDB.nombre = productoForm.nombre;
            productoDB.categoria = productoForm.categoria;
            productoDB.descripcion = productoForm.descripcion;
            productoDB.precio = productoForm.precio;
            productoDB.stock = productoForm.stock;
            Db.Productos.Update(productoDB);
            Db.SaveChanges();
        }

        private void AltaProductoEliminadoEnDb(Producto productoForm){
            var productoDb = Db.Productos.Include(prod => prod.imagenes.Where(img => img.eliminada==false)).Include(prod => prod.categoria).Where(prod => prod.id == productoForm.id).FirstOrDefault();
            if(productoDb.eliminado==true){
                productoDb.eliminado=false;
                Db.Productos.Update(productoDb);
                Db.SaveChanges();
            }
        }

        private bool LosArchivosSonSoloImagenes(Producto producto){
            foreach(var img in producto.listaImagenes){
                if(!img.ContentType.Contains("image")){
                    ModelState.AddModelError("imagenes", "Los archivos subidos deben ser imagenes.");
                    return false;
                }
            }
            return true;
        }

        private void AgregarImagenes(Producto producto){
            //validar si la imagen ya existe en la base de datos.
            foreach(var img in producto.listaImagenes){
                if(!ExisteImagenEnDb(producto, img)){
                    string nombreImagen = img.FileName;
                    string carpeta = Path.Combine(Hosting.WebRootPath, "imagenes");
                    string rutaDestino = Path.Combine(carpeta, nombreImagen);
                    img.CopyTo(new FileStream(rutaDestino, FileMode.Create));
                    producto.agregarImagen(new Imagen(nombreImagen));
                    producto.imagenes.Last().productoId=producto.id;
                    Db.Imagenes.Add(producto.imagenes.Last());
                    Db.SaveChanges();
                }
            }
        }

        private bool ExisteImagenEnDb(Producto producto, IFormFile imagen){
            var imagenesDb = Db.Imagenes.Where(img => img.productoId == producto.id).ToList();
            foreach(var imgDb in imagenesDb){
                if(imgDb.contenido.Equals(imagen.FileName)){
                    imgDb.eliminada=false;
                    Db.Imagenes.Update(imgDb);
                    Db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private Categoria AsignarCategoria(Producto producto){
            return Db.Categorias.Where(cat => cat.idCategoria == producto.categoria.idCategoria).FirstOrDefault();
        }

        private void QuitarImagenes(Producto producto){
            //setea la imagen de la base en false
            var imagenesDb = Db.Imagenes.Where(img => img.productoId == producto.id && img.eliminada==false).ToList();
            for(int i=0; i<producto.imagenes.Count;i++){
                if(producto.imagenes[i].contenido is null){
                    imagenesDb[i].eliminada=true;
                    Db.Imagenes.Update(imagenesDb[i]);
                    Db.SaveChanges();
                }
            }
        }

        private void ReconstruirImagenes(Producto producto){
            var imagenesAnteriores = Db.Imagenes.Where(img => img.productoId == producto.id && img.eliminada==false).ToList();
            producto.imagenes.Clear();
            producto.agregarListaImagen(imagenesAnteriores);
        }

        private bool NoTodosLosElementosSonNull(List<Imagen> imagenes){
            int contador = 0;
            if(!(imagenes is null)){
                foreach(var img in imagenes){
                    if(img.contenido is null){
                        contador++;
                    }
                }
                if(contador == imagenes.Count){
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool TieneAlgunIFormFile(Producto producto){
            if(producto.listaImagenes is null){
                return false;
            }
            return true;
        }

        private bool ImagenesContieneNull(List<Imagen> imagenes){
            foreach(var img in imagenes){
                if(img.contenido is null){
                    return true;
                }
            }
            return false;
        }
    }
}