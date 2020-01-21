using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MercadoVentasTP.Controllers
{
    public class PublicacionController : Controller
    {
        private int cantRegistrosPorPagina = 9;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Publicacion/Index
        [Authorize]
        public ActionResult Index(int pagina = 1)
        {
            ViewBag.Titulo = "Configuracion";
            ViewBag.Subtitulo = "Desde acá vas a poder administrar tus publicaciones";
            ViewBag.Parrafo = "Con solo dos clicks podés editar tus preferencias!";
            TempData["Anterior"] = "Manage";

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            var activas = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.Stock > 0 && p.IdUsuario == idUsuario && p.Estado == "Activa")
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();

            var paginatorVM = new IndexViewModel
            {
                ConfPrecioActual = userProp.ConfPrecioActual,
                PaginaActual = pagina,
                TotalRegistros = db.Publicaciones.Count(),
                RegistrosPorPagina = cantRegistrosPorPagina,
                PublicacionesActivas = activas,
                Filtro = new RouteValueDictionary()
            };
            return View(paginatorVM);
        }

        //
        // GET: /Publicacion/SinStock
        [Authorize]
        public ActionResult SinStock(int pagina = 1)
        {
            ViewBag.Titulo = "Sin stock";
            ViewBag.Subtitulo = "Desde acá vas a poder administrar tus publicaciones sin stock";
            ViewBag.Parrafo = "Con solo dos clicks podés administrar las publicaciones sin stock!";

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            var sinstock = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.Stock == 0 && p.IdUsuario == idUsuario && p.Estado == "Sin stock")
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();

            var paginatorVM = new IndexViewModel
            {
                ConfPrecioActual = userProp.ConfPrecioActual,
                PaginaActual = pagina,
                TotalRegistros = db.Publicaciones.Count(),
                RegistrosPorPagina = cantRegistrosPorPagina,
                PublicacionesActivas = sinstock,
                Filtro = new RouteValueDictionary()
            };
            return View(paginatorVM);
        }

        //
        // GET: /Publicacion/Inactivas
        [Authorize]
        public ActionResult Inactivas(int pagina = 1)
        {
            ViewBag.Titulo = "Sin stock";
            ViewBag.Subtitulo = "Desde acá vas a poder administrar tus publicaciones inactivas";
            ViewBag.Parrafo = "Con solo dos clicks podés activarlas para ser vendidas!";

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            var inactivas = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.IdUsuario == idUsuario && p.Estado == "Inactiva")
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();

            var paginatorVM = new IndexViewModel
            {
                ConfPrecioActual = userProp.ConfPrecioActual,
                PaginaActual = pagina,
                TotalRegistros = db.Publicaciones.Count(),
                RegistrosPorPagina = cantRegistrosPorPagina,
                PublicacionesActivas = inactivas,
                Filtro = new RouteValueDictionary()
            };
            return View(paginatorVM);
        }

        //
        // GET: /Publicacion/ActivarTodos
        [Authorize]
        public ActionResult ActivarTodos()
        {
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where((p => p.IdUsuario == idUsuario && p.Estado == "Inactiva"))
                        .ToList()
                         .ForEach(p => p.Estado = "Activa");
            db.SaveChanges();

        
            TempData["Success"] = "Las publicaciones han sido activadas correctamente.";

            return RedirectToAction("Index", "Publicacion");
        }

        // GET: Publicacion/Activar/5
        [Authorize]
        public ActionResult Activar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            publicacion.Estado = "Activa";
            
            db.SaveChanges();
            return RedirectToAction("Index", "Publicacion");
        }

        // GET: Publicacion/Details/5
        public ActionResult Detail(int? id)
        {
            ViewBag.Titulo = "Detalle";
            ViewBag.Subtitulo = "Desde acá vas a poder visualizar el detalle de una publicación";
            ViewBag.Parrafo = "Con solo dos clicks podés comprar la cantidad de artículos que necesites!";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: Publicacion/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            ViewBag.Titulo = "Detalle";
            ViewBag.Subtitulo = "Desde acá vas a poder visualizar en detalle tu publicación";
            ViewBag.Parrafo = "Con solo dos clicks podés editar todo lo que necesites!";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: Publicacion/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Titulo = "Nueva publicacion";
            ViewBag.Subtitulo = "Desde acá vas a poder generar una nueva publicacion";
            ViewBag.Parrafo = "Agrega la mayor cantidad de datos posibles para que tu publicación tenga muchas compras!";

            var productosConCategorias = ObtenerProductoConCategoria(db.Productos.OrderBy(c => c.Nombre).ToList());
            var productosSeleccionados = new List<Producto>();
            foreach (var idProducto in productosConCategorias)
            {
                productosSeleccionados.Add(db.Productos.FirstOrDefault(p => p.Id == idProducto));
            }
            ViewBag.Categorias = this.ObtenerCategorias(productosSeleccionados);
            ViewBag.Productos = this.ObtenerProductosPorCategoria(productosSeleccionados.First().IdCategoria); 
            return View();
        }

        //
        // POST: Publicacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdCategoria, IdProducto, Titulo, Descripcion, Stock, Precio, PrecioMin, PrecioMax, Estado")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
                publicacion.IdUsuario = userProp.IdUsuario;
                publicacion.PrecioActual = publicacion.PrecioMin;
                if (publicacion.Stock <= 0)
                {
                    publicacion.Stock = 0;
                    publicacion.Estado = "Sin stock";
                }
                db.Publicaciones.Add(publicacion);
                db.SaveChanges();
                return RedirectToAction("Index", "Publicacion");
            }
            return View(publicacion);
        }

        // GET: Publicacion/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Titulo = "Edicion";
            ViewBag.Subtitulo = "Desde acá vas a poder modificar cualquier dato de tu publicación";
            ViewBag.Parrafo = "Con solo dos clicks podés editar todo lo que necesites!";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        //
        // POST: Publicacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id, IdCategoria, IdProducto, IdUsuario, Titulo, Descripcion, Precio, PrecioMin, PrecioMax, Stock, Estado, PrecioActual, Cantidad")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                if (publicacion.Estado == "Sin stock")
                {
                    publicacion.Stock = 0;
                }
                else
                {
                    if (publicacion.Estado == "Activa" && publicacion.Stock <= 0)
                    {
                        publicacion.Stock = 1;
                    }
                }
                db.Entry(publicacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "La publicación ha sido modificada correctamente.";
                return RedirectToAction("Details", "Publicacion", new { id = publicacion.Id});
            }
            return View(publicacion);
        }

        // GET: Publicacion/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        //
        // POST: Publicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacion publicacion = db.Publicaciones.Find(id);
            db.Publicaciones.Remove(publicacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: Publicacion/ObtenerProductos/categoria
        // Lo consume el js de Create en Publicacion
        [HttpGet]
        public ActionResult ObtenerProductos(string idCategoria)
        {
            var types = this.ObtenerProductosPorCategoria(idCategoria);
            return Json(types, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> ObtenerCategorias(List<Producto> productos)
        {
            List<SelectListItem> categorias = new List<SelectListItem>();
            foreach (var producto in productos)
            {
                var categoria = db.Categorias.Where(c => c.Id == producto.IdCategoria).FirstOrDefault();
                categorias.Add(new SelectListItem() { Value = categoria.Id.ToString(), Text = categoria.Nombre });
            }
            return categorias.GroupBy(x => x.Text).Select(x => x.First()).ToList();
        }

        private List<int> ObtenerProductoConCategoria(List<Producto> productos)
        {
            return db.Productos.Select(p => p.Id).Intersect(productos.Select(c => c.Id)).ToList();
        }

        private List<SelectListItem> ObtenerProductosPorCategoria(string idCategoria)
        {
            var productos = db.Productos.OrderBy(p => p.IdCategoria).Where(p => p.IdCategoria == idCategoria).ToList();
            var resp = productos.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nombre
            }).ToList();

            resp.Insert(0, new SelectListItem() { Value = "", Text = "Selecciona un producto" });
            return resp;
        }

        //
        // GET: /Publicacion/Grafico
        [Authorize]
        public ActionResult Grafico()
        {
            ViewBag.Titulo = "Reporte de ventas";
            ViewBag.Subtitulo = "Desde acá vas a poder visualizar las estadísticas de ventas de tus publicaciones";
            ViewBag.Parrafo = "Con solo dos clicks podés conocer en detalle el estado de tus ventas!";
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerClaves ()
        {
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            var tienes = db.Tiene.GroupBy(t => t.IdPublicacion).ToList();
            var misClaves = new List<string>();
            foreach (var tiene in tienes)
            {
                var idPub = tiene.Key;
                var cantidad = 0;
                foreach (var item in tiene)
                {
                    cantidad++;
                }
                misClaves.Add(idPub.ToString());
            }
            var resp = Json(misClaves, JsonRequestBehavior.AllowGet);
            return resp;
        }

        [HttpGet]
        public ActionResult ObtenerValores()
        {
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = userProp.IdUsuario;
            var tienes = db.Tiene.GroupBy(t => t.IdPublicacion).ToList();
            var misValores = new List<string>();
            foreach (var tiene in tienes)
            {
                var idPub = tiene.Key;
                var cantidad = 0;
                foreach (var item in tiene)
                {
                    cantidad++;
                }
                misValores.Add(cantidad.ToString());
            }
            var resp =Json(misValores, JsonRequestBehavior.AllowGet);
            return resp;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
