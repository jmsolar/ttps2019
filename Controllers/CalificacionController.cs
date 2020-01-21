using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MercadoVentasTP.Controllers
{
    public class CalificacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

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

        [HttpGet]
        [Authorize]
        public ActionResult Index() {
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var IdUser = userProp.IdUsuario;
            var comprasRealizadasSinCalificar = db.Compras.Where(c => c.IdUsuario == IdUser).Join(db.Tiene, c => c.Id, t => t.IdCompra, (c, t) => new {c, t}).ToList();
            List<Compra> comprasSC = new List<Compra>();

            foreach (var subItem in comprasRealizadasSinCalificar)
            {
                Compra itemData = subItem.c;
                Tiene itemTiene = subItem.t;
                if (itemTiene.Estado == "Sin calificar")
                {
                    comprasSC.Add(itemData);
                }
            }

            return View(comprasSC);
        }
        
        //
        // GET: Movimiento/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: Calificacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                // guardo la calificacion
                var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
                var IdUser = userProp.IdUsuario;
                calificacion.IdUsuarioCalificador = IdUser;
                db.Calificaciones.Add(calificacion);

                // obtengo el idPublicación y actualizo el estado a calificado en la tabla Tiene
                var idPublicacion = calificacion.IdPublicacion;
                var idCompra = calificacion.IdCompra;

                var tiene = db.Tiene.Where(ti => ti.IdPublicacion == idPublicacion).Where(ti => ti.IdCompra == idCompra).First();
                tiene.Estado = "Calificado";
              
               // Tiene.Update(userProp);

                db.SaveChanges();
                TempData["Success"] = "La calificación ha sido creada correctamente.";
                return RedirectToAction("Index");                
            }
            return View(calificacion);
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