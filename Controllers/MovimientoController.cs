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
    public class MovimientoController : Controller
    {
    
        private ApplicationUserManager _userManager;

        private int cantRegistrosPorPagina = 10;

        private ApplicationDbContext db = new ApplicationDbContext();

        public int totalDeRegistros { get; private set; }

        public List<Movimiento> movimientos { get; private set; }


        // GET: Movimiento
        [Authorize]
        [HttpGet]
        public ActionResult Index(int pagina = 1) {

            this.SetInitialInfo();
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var IdUser = userProp.IdUsuario;

            movimientos = db.Movimientos
            .Where(m => m.IdUsuario == IdUser)
            .OrderByDescending(p => p.Fecha)
            .Skip((pagina - 1) * cantRegistrosPorPagina)
            .Take(cantRegistrosPorPagina)
            .ToList();

            var paginadorVM = this.ObtenerModelo(pagina);
            return View(paginadorVM);

            /*
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var IdUser = userProp.IdUsuario;
            var movimientos = db.Movimientos.Where(m => m.IdUsuario == IdUser).OrderByDescending(m => m.Fecha).ToList();
            return View(movimientos);*/
        }

        [HttpPost]
        public ActionResult Index(Movimiento movimiento)
        {
            db.Movimientos.Add(movimiento);
            db.SaveChanges();
           
            return View();
        }

        // GET: Movimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento Movimiento = db.Movimientos.Find(id);
            if (Movimiento == null)
            {
                return HttpNotFound();
            }
            return View(Movimiento);
        }


        private void SetInitialInfo()
        {
            /* ViewBag.Titulo = "Bienvenido!";
            ViewBag.Subtitulo = "En este sitio vas a encontrar lo que necesitas en pocos clicks, animate!";
            ViewBag.Parrafo = "Comenza a navegar y agrega lo que necesites a tu carrito";
            ViewBag.Registrar = "Registrarme";
            TempData["Anterior"] = "Home"; */

            totalDeRegistros = db.Movimientos.Count();
            movimientos = new List<Movimiento>();
        }


        private MovimientoVM ObtenerModelo(int pagina)
        {
            var paginatorVM = new MovimientoVM
            {
                PaginaActual = pagina,
                TotalRegistros = totalDeRegistros,
                RegistrosPorPagina = cantRegistrosPorPagina,
                Movimientos = movimientos,
                Filtro = new RouteValueDictionary()
            };
            return paginatorVM;
        }



        // GET: Movimiento/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


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
        // POST: Movimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movimiento movimiento)
        {
            if (ModelState.IsValid) {
                var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
                var IdUser = userProp.IdUsuario;
                movimiento.IdUsuario = IdUser;
                db.Movimientos.Add(movimiento);
                
                if (movimiento.Operacion == "Depósito") {
                    // obtengo el saldo actual del usuario y lo actualizo
                    userProp.SaldoActual = userProp.SaldoActual + movimiento.Monto;
                    TempData["Success"] = "El movimiento ha sido generado correctamente.";
                    UserManager.Update(userProp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else {              
                    // si hay saldo suficiente, se hace la extracción
                    if (userProp.SaldoActual >= movimiento.Monto) { 
                        userProp.SaldoActual = userProp.SaldoActual - movimiento.Monto;
                        TempData["Success"] = "El movimiento ha sido generado correctamente.";
                        UserManager.Update(userProp);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    } else {
                        // no se puede realizar la extracción
                        ViewData["Error"] = "El saldo en la billetera es insuficiente para realizar la extracción solicitada";
                        return View("Create");
                    }
                }                             
              //  UserManager.Update(userProp);              
               // db.SaveChanges();                      
               
            }
            return View(movimiento);   
        }

        // GET: Movimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento Movimiento = db.Movimientos.Find(id);
            if (Movimiento == null)
            {
                return HttpNotFound();
            }
            return View(Movimiento);
        }

        // POST: Movimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdBilletera,Fecha")] Movimiento Movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Movimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Movimiento);
        }

        // GET: Movimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento Movimiento = db.Movimientos.Find(id);
            if (Movimiento == null)
            {
                return HttpNotFound();
            }
            return View(Movimiento);
        }

        // POST: Movimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimiento Movimiento = db.Movimientos.Find(id);
            db.Movimientos.Remove(Movimiento);
            db.SaveChanges();
            return RedirectToAction("Index");
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

