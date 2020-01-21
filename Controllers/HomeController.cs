using Microsoft.AspNet.Identity;
using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MercadoVentasTP.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;

        private int cantRegistrosPorPagina = 9;
        
        private ApplicationDbContext db = new ApplicationDbContext();

        public int totalDeRegistros { get; private set; }

        public List<Publicacion> publicaciones { get; private set; }

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
        // GET: /Home/_Header
        public PartialViewResult _Header()
        {
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            if (userProp != null)
            {
                ViewBag.saldo = userProp.SaldoActual;
            }

            return PartialView("~/Views/Shared/_Header.cshtml");
        }

        [HttpGet]
        public ActionResult Index(int pagina = 1)
        {
            this.SetInitialInfo();
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = 0;
            if (userProp != null)
            {
                idUsuario = userProp.IdUsuario;
                publicaciones = db.Publicaciones
                .OrderBy(p => p.Id)
                .Where(p => p.Estado == "Activa" && p.IdUsuario != idUsuario)
                .Skip((pagina - 1) * cantRegistrosPorPagina)
                .Take(cantRegistrosPorPagina)
                .ToList();
            }
            else
            {
                publicaciones = db.Publicaciones
                .OrderBy(p => p.Id)
                .Where(p => p.Estado == "Activa")
                .Skip((pagina - 1) * cantRegistrosPorPagina)
                .Take(cantRegistrosPorPagina)
                .ToList();
            }

            var paginadorVM = this.ObtenerModelo(pagina);
            return View(paginadorVM);
        }

        [HttpPost]
        public ActionResult Index(string busqueda, int pagina = 1)
        {
            this.SetInitialInfo();
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var idUsuario = 0;
            if (busqueda != null)
            {
                if (userProp != null)
                {
                    idUsuario = userProp.IdUsuario;
                    publicaciones = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.Titulo.Contains(busqueda) && p.Estado == "Activa" && p.Stock > 0 && p.IdUsuario != idUsuario || p.Descripcion.Contains(busqueda) && p.Estado == "Activa" && p.Stock > 0)
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();
                }
                else
                {
                    publicaciones = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.Titulo.Contains(busqueda) && p.Estado == "Activa" && p.Stock > 0 || p.Descripcion.Contains(busqueda) && p.Estado == "Activa" && p.Stock > 0)
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();
                }
                totalDeRegistros = publicaciones.Count();
            }
            else
            {
                if (userProp != null)
                {
                    idUsuario = userProp.IdUsuario;
                    publicaciones = db.Publicaciones
                        .OrderBy(p => p.Id)
                        .Where(p => p.Estado == "Activa" && p.Stock > 0 && p.IdUsuario != idUsuario)
                        .Skip((pagina - 1) * cantRegistrosPorPagina)
                        .Take(cantRegistrosPorPagina)
                        .ToList();
                }
                else
                {
                    publicaciones = db.Publicaciones
                    .OrderBy(p => p.Id)
                    .Where(p => p.Estado == "Activa")
                    .Skip((pagina - 1) * cantRegistrosPorPagina)
                    .Take(cantRegistrosPorPagina)
                    .ToList();
                }
            }

            ViewBag.TituloBusqueda = "Se encontraron " + totalDeRegistros.ToString() + " publicaciones para la búsqueda ";
            ViewBag.FiltroBusqueda = busqueda;

            var paginadorVM = this.ObtenerModelo(pagina);
            paginadorVM.Filtro["busqueda"] = busqueda;
            return View(paginadorVM);
        }

        private void SetInitialInfo() {
            ViewBag.Titulo = "Bienvenido!";
            ViewBag.Subtitulo = "En este sitio vas a encontrar lo que necesitas en pocos clicks, animate!";
            ViewBag.Parrafo = "Comenza a navegar y agrega lo que necesites a tu carrito";
            ViewBag.Registrar = "Registrarme";
            TempData ["Anterior"] = "Home";

            totalDeRegistros = db.Publicaciones.Count();
            publicaciones = new List<Publicacion>();
        }
            
        private IndexVM ObtenerModelo(int pagina)
        {
            var paginatorVM = new IndexVM
            {
                PaginaActual = pagina,
                TotalRegistros = totalDeRegistros,
                RegistrosPorPagina = cantRegistrosPorPagina,
                Publicaciones = publicaciones,
                Filtro = new RouteValueDictionary()
            };
            return paginatorVM;
        }
    }
}