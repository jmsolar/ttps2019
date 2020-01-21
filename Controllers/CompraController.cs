using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MercadoVentasTP.Controllers
{
    [Authorize]
    public class CompraController : Controller
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

        // GET: Compra
        public ActionResult Index()
        {
            return View(db.Compras.ToList().OrderByDescending(c => c.Id));
        }

        // GET: Compra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }


        // POST: Compra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Monto")] Compra compra)
        {
            //db.Carrito.Add();

            //db.SaveChanges();
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());

            if (userProp.SaldoActual > compra.Monto)
            {
                var IdUser = userProp.IdUsuario;
                userProp.SaldoActual = userProp.SaldoActual - compra.Monto;
                UserManager.Update(userProp);

                db.Compras.Add(compra);
                db.SaveChanges();
                compra.Fecha = new DateTime();
                compra.IdUsuario = IdUser;
                db.Compras.Add(compra);
                //List<Publicacion> publicacionesCarrito = Session["publicacionesCarrito"] as List<Publicacion>;
            }
            else
            {
                Session["Error"] = "El saldo es insuficiente para realizar la compra.";
                return RedirectToAction("Index", "Carrito");
            }

            return View(db.Compras.ToList().OrderByDescending(c => c.Id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "Id,Cantidad,Monto,Fecha")] Compra compra)
        {

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
			var IdUser = userProp.IdUsuario;

            //var montoTotal = db.Carrito.Where(c => c.IdUsuario == IdUser);


            var ItemsCarrito = db.Carrito.Where(c => c.IdUsuario == IdUser).Join(db.Publicaciones, c => c.IdPublicacion, p => p.Id, (carrito, publicacion) => new { carrito, publicacion }).ToArray();
            float montoTotal = 0;
            List<Tiene> itemsTiene = new List<Tiene>();
            List<Publicacion> publicaciones = new List<Publicacion>();
            foreach (var subItem in ItemsCarrito)
            {
                var itemData = subItem.publicacion;
                var carritoData = subItem.carrito;
                Tiene tiene = new Tiene();
                montoTotal = montoTotal + (itemData.PrecioActual * carritoData.Cantidad);
                tiene.Cantidad = carritoData.Cantidad;
                tiene.IdPublicacion = carritoData.IdPublicacion;
                //Precio unitario
                tiene.Monto = itemData.PrecioActual;
                tiene.NombrePublicacion = itemData.Titulo;
                itemsTiene.Add(tiene);
                itemData.Stock = itemData.Stock - carritoData.Cantidad;
                if (itemData.Stock < 0)
                {
                    TempData["Error"] = "Stock insuficiente para: " + itemData.Titulo;
                    return RedirectToAction("Index", "Carrito");
                }
                else if(itemData.Stock == 0)
                {
                    itemData.Estado = "Sin Stock";
                }
                publicaciones.Add(itemData);

			}

            if (userProp.SaldoActual >= montoTotal)
            {
                userProp.SaldoActual = userProp.SaldoActual - montoTotal;
                UserManager.Update(userProp);
                compra.IdUsuario = IdUser;
                compra.Monto = montoTotal;
                db.Compras.Add(compra);
                db.SaveChanges();

                publicaciones.ForEach(publicacion => db.Entry(publicacion).State = EntityState.Modified);
                db.SaveChanges();


                Movimiento movimiento = new Movimiento();
                movimiento.Fecha = compra.Fecha;
                movimiento.IdUsuario = IdUser;
                movimiento.Operacion = "Compra";
                movimiento.Monto = montoTotal;
                db.Movimientos.Add(movimiento);
                db.SaveChanges();
                

                var idCompra = compra.Id;
                itemsTiene.ForEach(item => item.IdCompra = idCompra);
                db.Tiene.AddRange(itemsTiene);
                db.SaveChanges();
                
                var items = db.Carrito.Where(item => item.IdUsuario == IdUser);
                db.Carrito.RemoveRange(items);
                db.SaveChanges();
                Session["itemsCarrito"] = "0";
            }
            else
            {
                TempData["Error"] = "El saldo es insuficiente para realizar la compra.";
                return RedirectToAction("Index", "Carrito");
            }
            TempData["Success"] = "La compra se ha efectuado de manera exitosa. Si quieres puedes calificarla";
            return RedirectToAction("Index", "Calificacion", View(db.Compras.Where(c => c.IdUsuario == IdUser).ToList()));

        }


        // GET: Compra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUsuario,Fecha,Monto")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compra);
        }

        // GET: Compra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
